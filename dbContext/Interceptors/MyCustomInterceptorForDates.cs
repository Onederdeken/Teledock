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
            var entries = eventData.Context.ChangeTracker.Entries().Where(e=>e.Entity is Client && (e.State == EntityState.Added || e.State == EntityState.Modified) ||
            e.Entity is Founder && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                if(entry.State == EntityState.Added)
                {
                    if(entry.GetType() == typeof(Client))
                    {
                        ((Client)entry.Entity).dateAdd = DateOnly.FromDateTime(DateTime.Now);
                    }
                    else
                    {
                        ((Founder)entry.Entity).dateAdd = DateOnly.FromDateTime(DateTime.Now);
                    }
                }
                else
                {
                    if (entry.GetType() == typeof(Client))
                    {
                        ((Client)entry.Entity).dateUpdate = DateOnly.FromDateTime(DateTime.Now);
                    }
                    else
                    {
                        ((Founder)entry.Entity).dateUpdate = DateOnly.FromDateTime(DateTime.Now);
                    }
                }
            }
            return await base.SavingChangesAsync(eventData, result, token);
        }
    }
}
