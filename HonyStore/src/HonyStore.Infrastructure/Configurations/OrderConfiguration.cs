using HonyStore.Domain.Orders;
using HonyStore.Domain.Products;
using HonyStore.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HonyStore.Infrastructure.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("orders");

            builder.HasKey(o => o.Id);

            //id
            builder.Property(o => o.Id)
                .HasConversion(
                    Id => Id.Value,
                    Value => OrderId.Create(Value)
                )
                .IsRequired();

            //products
            builder.OwnsMany(o => o.Products,
                builder =>
                {
                    builder.ToJson();

                    builder.Property(p => p.ProductId)
                        .HasConversion(
                            Id => Id.Value,
                            Value => ProductId.Create(Value)
                        );

                    builder.Ignore(p => p.TotalPrice);
                }
            );

            builder.Navigation(o => o.Products)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
