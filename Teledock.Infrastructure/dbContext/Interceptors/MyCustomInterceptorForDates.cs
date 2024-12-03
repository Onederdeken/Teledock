using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Teledock.Domain.Models;
namespace Teledock.Infrastructure.dbContext.Interceptors
{
    public class MyCustomInterceptorForDates:SaveChangesInterceptor
    {
        //добавление даты создания записи и добавление/обновление даты обновления записи инкапсулировал в перехватчике события сохранения базы данных
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken token = default)
        {
            try
            {
                //находим сущности клиент или фаундер на состояние добавление или обновления
                var entries = eventData.Context.ChangeTracker.Entries().Where(e => e.Entity is Client && (e.State == EntityState.Added || e.State == EntityState.Modified) ||
                e.Entity is Founder && (e.State == EntityState.Added || e.State == EntityState.Modified));
                //пробегаемся по каждой сущности и если состояние добавления добавляем к записи дату создания если же обновление обновляем запись даты обновления сущности
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
