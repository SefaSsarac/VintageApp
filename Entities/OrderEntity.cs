using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VintageApp.Data.Entities.BaseEntity;

namespace VintageApp.Data.Entities
{
    public class OrderEntity : BaseEntity
    {
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        
        
        


        // Relational Property
        public List<OrderProductEntity> OrderProducts { get; set; }
        public UserEntity Customer { get; set; }
    }

    public class OrderConfiguration : BaseConfiguration<OrderEntity>
    {
       public override void Configure(EntityTypeBuilder<OrderEntity> builder)
       {          
            base.Configure(builder);
       }    
    }
}

