using Microsoft.EntityFrameworkCore;
using Teledock.Domain.Models;

namespace Teledock.Infrastructure.dbContext
{
    public class DbCommand:DbContext
    {
        public DbSet<Client> clients{get;set;}
        public DbSet<Founder> founders{get;set;}
        public DbCommand(DbContextOptions<DbCommand> options):base(options){}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>()
                .HasIndex(e => e.Inn)
                .IsUnique(); // ”никальный индекс на поле Inn
            modelBuilder.Entity<Founder>()
                .HasIndex(e=>e.Inn)
                .IsUnique();
        }
    }
}