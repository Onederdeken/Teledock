using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Teledock.Models;
namespace Teledock.dbContext.Interceptors
{
    public class MyCustomInterceptorForDates:SaveChangesInterceptor
    {
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken token = default)
        {
            try
            {
                var entries = eventData.Context.ChangeTracker.Entries().Where(e => e.Entity is Client && (e.State == EntityState.Added || e.State == EntityState.Modified) ||
                e.Entity is Founder && (e.State == EntityState.Added || e.State == EntityState.Modified));

                foreach (var entry in entries)
                {
                    if (entry.State == EntityState.Added)
                    {
                        if (entry.Entity is Client)
                        {
                            ((Client)entry.Entity).dateAdd = DateTime.UtcNow;

                        }
                        else
                        {
                            ((Founder)entry.Entity).dateAdd = DateTime.UtcNow;
                        }
                    }
                    else
                    {
                        if (entry.Entity is Client)
                        {
                            ((Client)entry.Entity).dateUpdate = DateTime.UtcNow;
                        }
                        else
                        {
                            ((Founder)entry.Entity).dateUpdate = DateTime.UtcNow;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
            return await base.SavingChangesAsync(eventData, result, token);
        }
    }
}
