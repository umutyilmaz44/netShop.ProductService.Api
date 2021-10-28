using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.ValueGeneration.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NetShop.ProductService.Domain.Entities;

namespace NetShop.ProductService.Infrastructure.Persistence.Content.Configuration
{
    public class BrandModelConfiguration : IEntityTypeConfiguration<BrandModel>
    {
        public void Configure(EntityTypeBuilder<BrandModel> builder)
        {
            builder.ToTable("BrandModels");

            builder.Property(s => s.Id)
                .HasColumnName("id")
                // FOR INMEMORY
                .HasValueGenerator<GuidValueGenerator>()
                // FOR POSTGRESQL
                // .UseIdentityColumn<Guid>()
                .IsRequired();

            builder.Property(s => s.brandId)
                .HasColumnName("brand_id")
                .IsRequired();

            builder.Property(s => s.modelName)
                .HasColumnName("model_name")
                .IsRequired();
        }
    }
}