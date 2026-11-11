using EMall.Data.EntityModels;
using EMall.Data.EntityModels.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMall.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
            => builder.HasData(this.GetSeedData());
        

#pragma warning disable CA1822 // Mark members as static
        private IEnumerable<Order> GetSeedData()
            =>[
                new()
                {
                    OrderId = 1,
                    CustomerName = "Angel Snopf",
                    CreatedOn = DateTime.Now,
                    Status = "Cancelled",
                    TotalPrice = 25.55M
                },
                new()
                {
                    OrderId = 2,
                    CustomerName = "Tsvetelin Marinov",
                    CreatedOn = DateTime.Now,
                    Status = "Processing",
                    TotalPrice = 666M
                },
                new()
                {
                    OrderId = 3,
                    CustomerName = "Martina Pavlova",
                    CreatedOn = DateTime.Now,
                    Status = "Shipped",
                    TotalPrice = 1555.65M
                }
            ];
        
#pragma warning restore CA1822 // Mark members as static
    }
}