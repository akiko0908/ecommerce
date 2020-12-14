using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    public class Product
    {
        [Key]
        [Display(Name = "Mã sản phẩm")]
        public int product_ID { get; set; }

        [Required(ErrorMessage = "Sản phẩm phải được nhập tên!!!")]
        [Display(Name = "Tên sản phẩm")]
        public string product_Name { get; set; }

        [Required(ErrorMessage = "Phải nhập giá cho sản phẩm!!!")]
        [Display(Name = "Giá SP")]
        [Range(10000, 100000000, ErrorMessage = "Giá sản phẩm từ 10,000 đến 100,000,000vnđ!!!")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal product_Price { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng sản phẩm!!!")]
        [Display(Name = "Số lượng")]
        [Range(1, 1000, ErrorMessage = "Số lượng sản phẩm từ 1 đến 1,000 SP!!!")]
        public int product_Quantity { get; set; }

        [Display(Name = "Hình ảnh")]
        public string product_Image { get; set; }

        [Display(Name = "Mô tả sản phẩm")]
        public string product_Description { get; set; }

        public int? brand_ID { get; set; }
        [ForeignKey("brand_ID")]
        public virtual Brand Brand { get; set; }

        public int? hdh_ID { get; set; }
        [ForeignKey("hdh_ID")]
        public virtual HeDieuHanh HeDieuHanh { get; set; }

        public int? supplier_ID { get; set; }
        [ForeignKey("supplier_ID")]
        public virtual Supplier Supplier { get; set; }

        public int? categories_ID { get; set; }
        [ForeignKey("categories_ID")]
        public virtual Categories Categories { get; set; }
    }
}
