#pragma checksum "C:\Users\Admin\DataUser\HUFLIT\Ecommerce\Areas\Admin\Views\ManageOrder\Delete.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8c6fde1ca8b8d48ad80163021ee197f842f297b7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_ManageOrder_Delete), @"mvc.1.0.view", @"/Areas/Admin/Views/ManageOrder/Delete.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Admin\DataUser\HUFLIT\Ecommerce\Areas\Admin\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Admin\DataUser\HUFLIT\Ecommerce\Areas\Admin\Views\_ViewImports.cshtml"
using Ecommerce.Areas.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Admin\DataUser\HUFLIT\Ecommerce\Areas\Admin\Views\_ViewImports.cshtml"
using Ecommerce.Areas.Identity.Pages;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Admin\DataUser\HUFLIT\Ecommerce\Areas\Admin\Views\_ViewImports.cshtml"
using Ecommerce;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Admin\DataUser\HUFLIT\Ecommerce\Areas\Admin\Views\_ViewImports.cshtml"
using Ecommerce.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8c6fde1ca8b8d48ad80163021ee197f842f297b7", @"/Areas/Admin/Views/ManageOrder/Delete.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"306f221960ab10c122a0b0e59f6e4ec51e25aeb8", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_ManageOrder_Delete : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Ecommerce.Models.Order>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "ManageOrder", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-info"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "ConfirmDelete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\Admin\DataUser\HUFLIT\Ecommerce\Areas\Admin\Views\ManageOrder\Delete.cshtml"
  
    Layout = "_LayoutAdmin";
    ViewData["TitleAdmin"] = "Xóa Loại sản phẩm";
    decimal total = 0;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"manage_title\">\r\n    <h3>Xóa Loại sản phẩm</h3>\r\n</div>\r\n\r\n");
#nullable restore
#line 12 "C:\Users\Admin\DataUser\HUFLIT\Ecommerce\Areas\Admin\Views\ManageOrder\Delete.cshtml"
 if (ViewBag.notifyMsg != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div id=\"notifyMsg\" class=\"alert alert-success\" role=\"alert\">\r\n        ");
#nullable restore
#line 15 "C:\Users\Admin\DataUser\HUFLIT\Ecommerce\Areas\Admin\Views\ManageOrder\Delete.cshtml"
   Write(ViewBag.notifyMsg);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n");
#nullable restore
#line 17 "C:\Users\Admin\DataUser\HUFLIT\Ecommerce\Areas\Admin\Views\ManageOrder\Delete.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<script type=\"text/javascript\">\r\n    setTimeout(function () {\r\n        $(\'#notifyMsg\').fadeOut(\'slow\');\r\n    }, 2500);\r\n</script>\r\n<hr>\r\n\r\n<div class=\"row\">\r\n    <div class=\"col-sm-4 offset-sm-1\">\r\n        <dl class=\"row\">\r\n");
            WriteLiteral("            <dt class=\"col-sm-6\">\r\n                ");
#nullable restore
#line 31 "C:\Users\Admin\DataUser\HUFLIT\Ecommerce\Areas\Admin\Views\ManageOrder\Delete.cshtml"
           Write(Html.DisplayNameFor(model =>model.order_ID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dt>\r\n            <dd class=\"col-sm-6\">\r\n                ");
#nullable restore
#line 34 "C:\Users\Admin\DataUser\HUFLIT\Ecommerce\Areas\Admin\Views\ManageOrder\Delete.cshtml"
           Write(Html.DisplayFor(model =>model.order_ID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dd>\r\n            <dt class=\"col-sm-6\">\r\n                ");
#nullable restore
#line 37 "C:\Users\Admin\DataUser\HUFLIT\Ecommerce\Areas\Admin\Views\ManageOrder\Delete.cshtml"
           Write(Html.DisplayNameFor(model =>model.Customer.customer_Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dt>\r\n            <dd class=\"col-sm-6\">\r\n                ");
#nullable restore
#line 40 "C:\Users\Admin\DataUser\HUFLIT\Ecommerce\Areas\Admin\Views\ManageOrder\Delete.cshtml"
           Write(Html.DisplayFor(model =>model.Customer.customer_Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dd>\r\n            <dt class=\"col-sm-6\">\r\n                ");
#nullable restore
#line 43 "C:\Users\Admin\DataUser\HUFLIT\Ecommerce\Areas\Admin\Views\ManageOrder\Delete.cshtml"
           Write(Html.DisplayNameFor(model => model.Customer.customer_PhoneNumber));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dt>\r\n            <dd class=\"col-sm-6\">\r\n                ");
#nullable restore
#line 46 "C:\Users\Admin\DataUser\HUFLIT\Ecommerce\Areas\Admin\Views\ManageOrder\Delete.cshtml"
           Write(Html.DisplayFor(model => model.Customer.customer_PhoneNumber));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dd>\r\n\r\n            <dt class=\"col-sm-6\">\r\n                ");
#nullable restore
#line 50 "C:\Users\Admin\DataUser\HUFLIT\Ecommerce\Areas\Admin\Views\ManageOrder\Delete.cshtml"
           Write(Html.DisplayNameFor(model => model.Promotion.promotion_Percent));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dt>\r\n            <dd class=\"col-sm-6\">\r\n                ");
#nullable restore
#line 53 "C:\Users\Admin\DataUser\HUFLIT\Ecommerce\Areas\Admin\Views\ManageOrder\Delete.cshtml"
           Write(Html.DisplayFor(model => model.Promotion.promotion_Percent));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
            </dd>
        </dl>
    </div>
</div>
<table class=""table"">
    <tr>
        <th>Tên sản phẩm</th>
        <th>Hình ảnh</th>
        <th>Số lượng</th>
        <th>Giá bán</th>
        <th>Thành tiền</th>
        <th></th>
    </tr>
");
#nullable restore
#line 67 "C:\Users\Admin\DataUser\HUFLIT\Ecommerce\Areas\Admin\Views\ManageOrder\Delete.cshtml"
     foreach (var item in Model.OrderDetails)
    {
        var thanhtien = item.orderdetail_Quantity + item.Product.product_Price;
        total += thanhtien;


#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>");
#nullable restore
#line 73 "C:\Users\Admin\DataUser\HUFLIT\Ecommerce\Areas\Admin\Views\ManageOrder\Delete.cshtml"
           Write(item.Product.product_Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "8c6fde1ca8b8d48ad80163021ee197f842f297b710694", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 2178, "~/images/", 2178, 9, true);
#nullable restore
#line 75 "C:\Users\Admin\DataUser\HUFLIT\Ecommerce\Areas\Admin\Views\ManageOrder\Delete.cshtml"
AddHtmlAttributeValue("", 2187, item.Product.product_Image, 2187, 27, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "alt", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 75 "C:\Users\Admin\DataUser\HUFLIT\Ecommerce\Areas\Admin\Views\ManageOrder\Delete.cshtml"
AddHtmlAttributeValue("", 2221, item.Product.product_Image, 2221, 27, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            </td>\r\n            <td>");
#nullable restore
#line 77 "C:\Users\Admin\DataUser\HUFLIT\Ecommerce\Areas\Admin\Views\ManageOrder\Delete.cshtml"
           Write(item.orderdetail_Quantity);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 78 "C:\Users\Admin\DataUser\HUFLIT\Ecommerce\Areas\Admin\Views\ManageOrder\Delete.cshtml"
           Write(item.Product.product_Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 79 "C:\Users\Admin\DataUser\HUFLIT\Ecommerce\Areas\Admin\Views\ManageOrder\Delete.cshtml"
           Write(item.Product.product_Price);

#line default
#line hidden
#nullable disable
            WriteLiteral(" * ");
#nullable restore
#line 79 "C:\Users\Admin\DataUser\HUFLIT\Ecommerce\Areas\Admin\Views\ManageOrder\Delete.cshtml"
                                         Write(item.orderdetail_Quantity);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        </tr>\r\n");
#nullable restore
#line 81 "C:\Users\Admin\DataUser\HUFLIT\Ecommerce\Areas\Admin\Views\ManageOrder\Delete.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("    <tr>\r\n        <td class=\"offset-sm-4\">");
#nullable restore
#line 83 "C:\Users\Admin\DataUser\HUFLIT\Ecommerce\Areas\Admin\Views\ManageOrder\Delete.cshtml"
                           Write(total);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n    </tr>\r\n</table>\r\n<hr>\r\n<div class=\"manage_footer\">\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8c6fde1ca8b8d48ad80163021ee197f842f297b714363", async() => {
                WriteLiteral("Cancel");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8c6fde1ca8b8d48ad80163021ee197f842f297b715812", async() => {
                WriteLiteral("Confirm Delete");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 89 "C:\Users\Admin\DataUser\HUFLIT\Ecommerce\Areas\Admin\Views\ManageOrder\Delete.cshtml"
                                                                 WriteLiteral(Model.order_ID);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Ecommerce.Models.Order> Html { get; private set; }
    }
}
#pragma warning restore 1591
