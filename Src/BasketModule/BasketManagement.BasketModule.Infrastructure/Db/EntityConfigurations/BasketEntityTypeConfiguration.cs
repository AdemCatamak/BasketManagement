using BasketManagement.BasketModule.Domain;
using BasketManagement.BasketModule.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasketManagement.BasketModule.Infrastructure.Db.EntityConfigurations
{
    public class BasketEntityTypeConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder.ToTable("Baskets", "dbo.Basket");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id)
                   .HasConversion(id => id.Value,
                                  s => new BasketId(s));

            builder.Property(m => m.AccountId)
                   .HasColumnName("AccountId");
            builder.Property(m => m.CreatedOn)
                   .HasColumnName("CreatedOn");
            builder.Property(m => m.UpdatedOn)
                   .HasColumnName("UpdatedOn");

            builder.HasMany(x => x.BasketLines)
                   .WithOne(line => line.Basket)
                   .HasForeignKey(x => x.BasketId);

            builder.Metadata
                   .FindNavigation(nameof(Basket.BasketLines))
                   .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}