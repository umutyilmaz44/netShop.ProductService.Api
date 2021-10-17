using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using netShop.Domain.Entities;

namespace netShop.Persistence.Content.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("products");

            builder.Property(s => s.Id)
                .HasColumnName("id")
                .UseIdentityColumn()
                .IsRequired();

            builder.Property(s => s.productCode)
                .HasColumnName("product_code")
                .IsRequired();

            builder.Property(s => s.productName)
                .HasColumnName("product_name")
                .IsRequired();
        }
    }
}