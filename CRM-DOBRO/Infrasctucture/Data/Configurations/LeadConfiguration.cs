using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrasctucture.Configurations
{
    /// <summary>
    /// Configuration file for links in the Leads table
    /// </summary>
    public class LeadConfiguration : IEntityTypeConfiguration<Lead>
    {
        public void Configure(EntityTypeBuilder<Lead> builder)
        {
            builder.HasKey(l => l.Id);

            builder
                .HasOne(l => l.Contact)
                .WithOne(c => c.Lead)
                .HasForeignKey<Lead>(l => l.ContactId);

            builder
                .HasOne(l => l.Saler)
                .WithMany(s => s.Leads)
                .HasForeignKey(l => l.SalerId);
        }
    }
}
