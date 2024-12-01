using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Teledock.Models;

namespace Teledock.dbContext
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