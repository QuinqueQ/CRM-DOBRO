namespace Infrasctucture.Data;

public class CRMDBContext(DbContextOptions<CRMDBContext> options) : DbContext(options)
{
    public DbSet<Contact> Contacts => Set<Contact>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Lead> Leads => Set<Lead>();
    public DbSet<Sale> Sales => Set<Sale>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ContactConfiguration());
        modelBuilder.ApplyConfiguration(new LeadConfiguration());
        modelBuilder.ApplyConfiguration(new SaleConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
