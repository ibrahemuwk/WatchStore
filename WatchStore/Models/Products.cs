using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WatchStore.Models
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [DisplayName("Brand Id")]
        public int BrandId { get; set; }
        
        [DisplayName("Brand Name")]
        public Brand Brand { get; set; }
        [Required]
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
        [Required]
        [DisplayName("Product Description")]
        public string ProductDescription { get; set; }
        
        [DisplayName("Product Image")]
        public byte[] ProductImage { get; set; }
        [Required]
        [DisplayName("Product Price")]
        public double ProductPrice { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DisplayName("Product Date")]
        public DateTime ProductDate { get; set; }
        
        [DisplayName("Product Sale")]
        public double ProductSale { get; set; }
        [Required]
        [DisplayName("Product Quantity")]
        public int ProductQuantity { get; set; }




    }
}