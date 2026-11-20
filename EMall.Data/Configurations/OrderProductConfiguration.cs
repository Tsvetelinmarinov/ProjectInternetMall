using EMall.Data.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMall.Data.Configurations
{
    public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
    {
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
        }
    }
}