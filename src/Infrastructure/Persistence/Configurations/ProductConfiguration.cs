using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Application.Common.Constants;

namespace ProductCatalog.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasMaxLength(ApplicationConstants.Product.NameMaxLength)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasMaxLength(ApplicationConstants.Product.DescriptionMaxLength);

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.Stock)
                .IsRequired();
        }
    }
} 