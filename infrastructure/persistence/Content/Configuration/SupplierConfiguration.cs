using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.ValueGeneration.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NetShop.ProductService.Domain.Entities;

namespace NetShop.ProductService.Infrastructure.Persistence.Content.Configuration
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("Suppliers");

            builder.Property(s => s.Id)
                .HasColumnName("id")
                // FOR INMEMORY
                .HasValueGenerator<GuidValueGenerator>()
                // FOR POSTGRESQL
                // .UseIdentityColumn<Guid>()
                .IsRequired();

            builder.Property(s => s.email)
                .HasColumnName("email")
                .IsRequired();

            builder.Property(s => s.fax)
                .HasColumnName("fax")
                .IsRequired();
            
            builder.Property(s => s.phone)
                .HasColumnName("phone")
                .IsRequired();
            
            builder.Property(s => s.supplierName)
                .HasColumnName("supplier_name")
                .IsRequired();
            
            builder.Property(s => s.website)
                .HasColumnName("website")
                .IsRequired();
        }
    }
}