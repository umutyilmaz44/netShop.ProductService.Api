using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.ValueGeneration.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using netShop.Domain.Entities;

namespace netShop.Persistence.Content.Configuration
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable("Brands");

            builder.Property(s => s.Id)
                .HasColumnName("id")
                // FOR INMEMORY
                .HasValueGenerator<GuidValueGenerator>()
                // FOR POSTGRESQL
                // .UseIdentityColumn<Guid>()
                .IsRequired();

            builder.Property(s => s.brandName)
                .HasColumnName("brand_name")
                .IsRequired();
        }
    }
}