using Microsoft.EntityFrameworkCore;
using Teledock.Models;

namespace Teledock.dbContext
{
    public class DbQuery:DbContext
    {
        public DbSet<Client> clients { get; set; }
        public DbSet<Founder> founders { get; set; }
        public DbQuery(DbContextOptions<DbQuery> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>()
                .HasIndex(e => e.Inn)
                .IsUnique(); // Уникальный индекс на поле Inn
            modelBuilder.Entity<Founder>()
                .HasIndex(e => e.Inn)
                .IsUnique();
        }
    }
}
