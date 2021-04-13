using BasketManagement.BasketModule.Domain;
using BasketManagement.BasketModule.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasketManagement.BasketModule.Infrastructure.Db.EntityConfigurations
{
    public class BasketLineEntityTypeConfiguration : IEntityTypeConfiguration<BasketLine>
    {
        public void Configure(EntityTypeBuilder<BasketLine> builder)
        {
            builder.ToTable("BasketLines", "dbo.Basket");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id)
                .HasColumnName("Id")
                .HasConversion(id => id.Value,
                    guid => new BasketLineId(guid));

            builder.OwnsOne(m => m.BasketItem)
                .Property(item => item.ProductId)
                .HasColumnName("ProductId");

            builder.OwnsOne(m => m.BasketItem)
                .Property(item => item.Quantity)
                .HasColumnName("Quantity");

            builder.Property(m => m.UpdatedOn)
                .HasColumnName("UpdatedOn");

            builder.Property(m => m.IsDeleted)
                .HasColumnName("IsDeleted");

            builder.Property(m => m.RowVersion)
                .HasColumnName("RowVersion")
                .IsRowVersion();
        }
    }
}