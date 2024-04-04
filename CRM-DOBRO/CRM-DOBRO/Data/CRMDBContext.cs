using CRM_DOBRO.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM_DOBRO.Data
{
    public class CRMDBContext(DbContextOptions<CRMDBContext> options) : DbContext(options)
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<Sale> Sales { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Saler)
                .WithMany() // Предполагается, что связь между Sale и User однонаправленная
                .HasForeignKey(s => s.SalerId)
                .OnDelete(DeleteBehavior.NoAction); // Ограничение на удаление
        }


    }
}
