using CRM_DOBRO.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM_DOBRO.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(c => c.Id);

            builder
                .HasOne(c => c.Marketing)
                .WithMany(m => m.Contacts)
                .HasForeignKey(c => c.MarketingId);
        }
    }
}
