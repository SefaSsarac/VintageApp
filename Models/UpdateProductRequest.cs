using System.ComponentModel.DataAnnotations;
using VintageApp.Data.Enums;

namespace VintageApp.WebApi.Models
{
    public class UpdateProductRequest
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public ProductType ProductType { get; set; }
    }
}
