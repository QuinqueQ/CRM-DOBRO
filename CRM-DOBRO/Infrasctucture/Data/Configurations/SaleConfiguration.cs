namespace Infrasctucture.Data.Configurations;

/// <summary>
/// Configuration file for links in the Sales table
/// </summary>
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
