using EMall.Data.EntityModels;
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
                    CreatedOn = new DateTime(2026, 11, 12, 19, 51, 00),
                    Status = "Cancelled",
                    TotalPrice = 6299.96M
                },
                new()
                {
                    OrderId = 2,
                    CustomerName = "Tsvetelin Marinov",
                    CreatedOn = new DateTime(2026, 11, 12, 19, 51, 00),
                    Status = "Processing",
                    TotalPrice = 1269.96M
                },
                new()
                {
                    OrderId = 3,
                    CustomerName = "Martina Pavlova",
                    CreatedOn = new DateTime(2026, 11, 12, 19, 51, 00),
                    Status = "Shipped",
                    TotalPrice = 199.96M
                }
            ];
        
#pragma warning restore CA1822 // Mark members as static
    }
}