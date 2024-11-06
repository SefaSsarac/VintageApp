using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageApp.Data.Enums;
using static VintageApp.Data.Entities.BaseEntity;

namespace VintageApp.Data.Entities
{
    public class ProductEntity : BaseEntity
    {       
               
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public ProductType ProductType { get; set; }



        // Relational Property
        public List<OrderProductEntity> OrderProducts { get; set; }

    }
    public class ProductConfiguration : BaseConfiguration<ProductEntity> 
    {
        public override void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.Property(x => x.ProductName)
                 .IsRequired();
            
            
            base.Configure(builder);
        }
    }

}
