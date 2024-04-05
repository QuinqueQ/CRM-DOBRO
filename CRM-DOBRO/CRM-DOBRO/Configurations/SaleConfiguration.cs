using CRM_DOBRO.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM_DOBRO.Configurations
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.HasKey(s => s.Id);

            builder
                 .HasOne(s => s.Lead)
                 .WithMany(l => l.Sales)
                 .HasForeignKey(s => s.LeadId)
                 .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(s => s.Saler)
                .WithMany(saler => saler.Sales)
                .HasForeignKey(s => s.SalerId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
