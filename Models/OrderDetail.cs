using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    public class OrderDetail
    {
        [Key]
        [Display(Name = "Mã chi tiết đơn hàng")]
        public int orderdetail_ID { get; set; }

        [Display(Name = "Số lượng")]
        [Range(1, 20, ErrorMessage = "Số lượng mua từ 1 đến 20 sản phẩm!!!")]
        public int orderdetail_Quantity { get; set; }

        public int? order_ID { get; set; }
        [ForeignKey("order_ID")]
        public virtual Order Order { get; set; }

        public int? product_ID { get; set; }
        [ForeignKey("product_ID")]
        public virtual Product Product { get; set; }
    }
}
