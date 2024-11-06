using System.ComponentModel.DataAnnotations;
using VintageApp.Data.Enums;

namespace VintageApp.WebApi.Models
{
    public class AddProductRequest
    {
        [Required]
        [Length(2, 100)]
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public ProductType ProductType { get; set; }
    }
}
