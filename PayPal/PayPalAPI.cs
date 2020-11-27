using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Ecommerce.PayPal
{
    public class PayPalAPI
    {
        public IConfiguration configuration { get; }

        public PayPalAPI(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public async Task<string> GetRedirectURLToPayPal(double total, string currency)
        {
            try
            {
                return Task.Run(async () =>
                {
                    HttpClient httpClient = GetPayPalHttpClient();
                    PayPalAccessToken accessToken = await GetPayPalAccessTokenAsync(httpClient);
                    PayPalPaymentCreatedResponse createdResponse = await CreatePayPalPaymentAsync(httpClient, accessToken, total, currency);
                    return createdResponse.links.First(x => x.rel == "approval_url").href;
                }).Result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex, "Failed to login to PayPal");
                return null;
            }
        }

        public async Task<PayPalPaymentExecuteResponse> ExecutePayment(string paymentID, string payerID)
        {
            try
            {
                HttpClient httpClient = GetPayPalHttpClient();
                PayPalAccessToken accessToken = await GetPayPalAccessTokenAsync(httpClient);
                return await ExecutePayPalPaymentAsync(httpClient, accessToken, paymentID, payerID);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex, "Failed to login to PayPal");
                return null;
            }
        }

        private HttpClient GetPayPalHttpClient()
        {
            string sandbox = configuration["PayPal:urlAPI"];
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(sandbox),
                Timeout = TimeSpan.FromSeconds(30)
            };
            return httpClient;
        }

        private async Task<PayPalAccessToken> GetPayPalAccessTokenAsync(HttpClient httpClient)
        {
            byte[] bytes = Encoding.GetEncoding("iso-8859-1").GetBytes($"{configuration["PayPal:clientID"]}:{configuration["PayPal:secret"]}");

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "/v1/oauth2/token");
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytes));

            var form = new Dictionary<string, string> { ["grant_type"] = "client_credentials" };

            request.Content = new FormUrlEncodedContent(form);

            HttpResponseMessage response = await httpClient.SendAsync(request);

            string content = await response.Content.ReadAsStringAsync();
            PayPalAccessToken accessToken = JsonConvert.DeserializeObject<PayPalAccessToken>(content);

            return accessToken;
        }

        public async Task<PayPalPaymentCreatedResponse> CreatePayPalPaymentAsync(HttpClient httpClient,
                                                                                 PayPalAccessToken accessToken,
                                                                                 double total,
                                                                                 string currency)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "/v1/payments/payment");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.access_token);

            var payment = JObject.FromObject(new
            {
                intent = "sale",
                redirect_urls = new
                {
                    return_url = configuration["PayPal:returnUrl"],
                    cancel_url = configuration["PayPal:cancelUrl"]
                },
                payer = new { payment_method = "PayPal" },
                transactions = JArray.FromObject(new[]
                {
                    new {
                        amount = new {
                            total = total,
                            currency = currency
                        }
                    }
                })
            });

            request.Content = new StringContent(JsonConvert.SerializeObject(payment), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.SendAsync(request);

            string content = await response.Content.ReadAsStringAsync();
            PayPalPaymentCreatedResponse payPalPaymentCreated = JsonConvert.DeserializeObject<PayPalPaymentCreatedResponse>(content);
            return payPalPaymentCreated;
        }

        private async Task<PayPalPaymentExecuteResponse> ExecutePayPalPaymentAsync(HttpClient httpClient,
                                                                                   PayPalAccessToken accessToken,
                                                                                   string paymentID,
                                                                                   string payerID)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"v1/payments/payment/{paymentID}/execute");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.access_token);

            var payment = JObject.FromObject(new { payer_id = payerID });

            request.Content = new StringContent(JsonConvert.SerializeObject(payment), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.SendAsync(request);
            string content = await response.Content.ReadAsStringAsync();
            PayPalPaymentExecuteResponse executedPayment = JsonConvert.DeserializeObject<PayPalPaymentExecuteResponse>(content);
            return executedPayment;
        }

    }
}