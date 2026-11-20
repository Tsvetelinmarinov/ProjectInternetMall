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
                    CustomerEmail = "angel.snopf@example.com",
                    CreatedOn = new DateTime(2026, 11, 12, 19, 51, 00),
                    Status = "Cancelled",
                    TotalPrice = 6299.96M,
                    AddressForShipping = "бул. Витоша 100, София"
                },
                new()
                {
                    OrderId = 2,
                    CustomerName = "Tsvetelin Marinov",
                    CustomerEmail = "tsvetelin.marinov@example.com",
                    CreatedOn = new DateTime(2026, 11, 12, 19, 51, 00),
                    Status = "Processing",
                    TotalPrice = 1269.96M,
                    AddressForShipping = "ул. Иван Вазов 25, Пловдив"
                },
                new()
                {
                    OrderId = 3,
                    CustomerName = "Martina Pavlova",
                    CustomerEmail = "martina.pavlova@example.com",
                    CreatedOn = new DateTime(2026, 11, 12, 19, 51, 00),
                    Status = "Shipped",
                    TotalPrice = 199.96M,
                    AddressForShipping = "ул. Александровска 40, Бургас"
                }
            ];
        
#pragma warning restore CA1822 // Mark members as static
    }
}