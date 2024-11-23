using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Teledock.Models;

namespace Teledock.dbContext
{
    public class Db:DbContext
    {
        public DbSet<Client> clients{get;set;}
        public DbSet<Founder> founders{get;set;}
        public Db(DbContextOptions<Db> options):base(options){}
        
    }
}