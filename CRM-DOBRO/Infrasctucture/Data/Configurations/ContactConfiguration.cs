namespace Infrasctucture.Data.Configurations
{
    /// <summary>
    /// Configuration file for links in the Contacts table
    /// </summary>
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
