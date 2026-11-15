using HonyStore.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HonyStore.Domain.Shared;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace HonyStore.Infrastructure.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("products");

            builder.HasKey(p => p.Id);

            //id
            builder.Property(p => p.Id)
                .HasConversion(
                    Id => Id.Value,
                    Value => ProductId.Create(Value)
                )
                .IsRequired();

            //price
            builder.Property(p => p.Price)
                .HasConversion(
                    Price => Price.Value,
                    price => ProductPrice.Create(price).Value 
                )
                .IsRequired();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(Constraints.MAX_LENGHT_TITLE);

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(Constraints.MAX_LENGHT_TITLE);

        }
    }
}
