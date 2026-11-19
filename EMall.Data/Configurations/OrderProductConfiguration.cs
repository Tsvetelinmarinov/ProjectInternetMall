using EMall.Data.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMall.Data.Configurations
{
    public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
    {
        private readonly List<OrderProduct> _ordersWithProductsSeedingData = [
            new()
            {
                OrderId = 1,
                ProductId = 1,
            },
            new()
            {
                OrderId = 1,
                ProductId = 2,
            },
            new()
            {
                OrderId = 1,
                ProductId = 3,
            },
            new()
            {
                OrderId = 1,
                ProductId = 4,
            },
            new()
            {
                OrderId = 2,
                ProductId = 5,
            },
            new()
            {
                OrderId = 2,
                ProductId = 6,
            },
            new()
            {
                OrderId = 2,
                ProductId = 7,
            },
            new()
            {
                OrderId = 2,
                ProductId = 8,
            },
            new()
            {
                OrderId = 3,
                ProductId = 9,
            },
            new()
            {
                OrderId = 3,
                ProductId = 10,
            },
            new()
            {
                OrderId = 3,
                ProductId = 11,
            },
            new()
            {
                OrderId = 3,
                ProductId = 12,
            },
        ];

        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.HasKey("OrderId", "ProductId");

            builder.HasOne((op) => op.Order)
                   .WithMany((order) => order.OrderProducts)
                   .HasForeignKey((op) => op.OrderId)
                   .HasConstraintName("__FK_OrderProduct_Order__OrderId__");

            builder.HasOne((op) => op.Product)
                   .WithMany((product) => product.OrderProducts)
                   .HasForeignKey((op) => op.ProductId)
                   .HasConstraintName("__FK_OrderProduct_Product__ProductId__");

            builder.HasData(this._ordersWithProductsSeedingData);
        }
    }
}