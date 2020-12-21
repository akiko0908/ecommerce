using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    public class Customer
    {
        [Key]
        [Display(Name = "Mã khách hàng")]
        public int customer_ID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên khách hàng!!!")]
        [Display(Name = "Tên khách hàng")]
        public string customer_Name { get; set; }

        [Required(ErrorMessage = "Phải nhập số điện thoại!!!")]
        [Display(Name = "Số điện thoại")]
        [StringLength(25, MinimumLength = 10, ErrorMessage = "Số điện thoại từ 10 đến 11 số!!!")]
        public string customer_PhoneNumber { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email!!!")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string customer_Email { get; set; }

        [Required(ErrorMessage = "Nhập địa chỉ giao hàng tại đây!!!")]
        [Display(Name = "Địa chỉ giao hàng")]
        public string customer_AddressShip { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
