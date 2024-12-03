using Microsoft.EntityFrameworkCore;
using System.Transactions;
using Teledock.Abstractions;
using Teledock.Infrastructure.dbContext;
using Teledock.Domain.Models;
namespace Teledock.Repositories
{
    public class FounderRepositories : IFounderRepository
    {
        private readonly DbCommand _dbCommand;
        private readonly DbQuery _dbQuery;
        public FounderRepositories(DbCommand db, DbQuery dbQuery)
        {
            this._dbCommand = db;
            this._dbQuery = dbQuery;
        }
        public async Task AddFounder(Founder founder)
        {
            using(var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    //добавляем запись
                    _dbCommand.Add(founder);
                    //сохраняем
                    await _dbCommand.SaveChangesAsync();
                    //вносим изменения в базу данных dbQuery дабы сохранить целостность
                    _dbQuery.Add(founder);
                    await _dbQuery.SaveChangesAsync();

                    //комминитм изменения
                    transaction.Complete();
                }
                catch (System.Exception ex)
                {
                    //отменяем изменения и выбрасываем исключение
                    throw new Exception("ошибка добавления учредителя для клиента" + "\n" + ex.Message);
                }
            }
        }

        public async Task ChangeClient(int FounderId, int ClientId)
        {
            using( var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    //находим запись про нужного учредителя
                    var Founder = _dbCommand.founders.Find(FounderId);
                    //меням поле ClientId
                    Founder.ClientId = ClientId;
                    //сохраняем изменения
                    await _dbCommand.SaveChangesAsync();

                    //добавляем Founder к контексту dbQuery
                    
                    _dbQuery.Entry(Founder).State = EntityState.Modified;
                    //сохраняем изменения
                    await _dbQuery.SaveChangesAsync();

                    //комитим все изменения
                    transaction.Complete();
                }
                catch (System.Exception ex)
                {
                    //отменяем все изменения и выбрасываем исключение
                    throw new Exception("ошибка смены клиента" + "\n" + ex.Message);
                }
            }
        }

        public async Task DeleteFounder(int FounderId)
        {
            using(var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    // находим нужную запись
                    var founder = await _dbCommand.founders.FindAsync(FounderId);
                    //удаляем ее
                    _dbCommand.founders.Remove(founder);
                    //сохраняем изменения
                    await _dbCommand.SaveChangesAsync();

                    //добавляем нужную запись к контексту dbQuery
                    _dbQuery.Attach(founder);
                    //указываем что она удалена
                    _dbQuery.Entry(founder).State = EntityState.Deleted;
                    //сохранаяем изменения
                    await _dbQuery.SaveChangesAsync();
                    //комитим изменения
                    transaction.Complete();
                }
                catch (System.Exception ex)
                {
                    //отменяем все изменения и выбрасываем исключение
                    throw new Exception("ошибка удаления учредителя" + "\n" + ex.Message);
                }
            }
        }
        public async Task<bool> ExistFounder(int FounderId)
        {
            var founder = await _dbCommand.founders.AsNoTracking().FirstOrDefaultAsync(c=>c.Id == FounderId);
            return founder == null ? false : true;
        }

        public async Task<List<Founder>> getAllFounders()
        {
            try
            {
                return await _dbCommand.founders.ToListAsync();
            }
            catch (System.Exception ex)
            {
                throw new Exception($"ошибка получения списка учредителей  \n {ex.Message}");
            }
        }

        public async Task<Founder> getFounderById(int FounderId)
        {
            try
            {
                return await _dbCommand.founders.FirstOrDefaultAsync(c => c.Id == FounderId);
            }
            catch (System.Exception ex)
            {
                throw new Exception("ошибка получения учредителя по id" + "\n" + ex.Message);
            }
        }

        public async Task UpdateFounder(Founder founder)
        {
            using(var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    //находим нужного учредителя
                    var Founder = _dbCommand.founders.Find(founder.Id);
                    //меняем его поля
                    Founder.FIO = founder.FIO;
                    Founder.Inn = founder.Inn;
                    //сохраняем изменения
                    await _dbCommand.SaveChangesAsync();

                    //добавляем нужного учредителя в контекст dbQuery и указываем что он был изменен
                    
                    _dbQuery.Entry(Founder).State=EntityState.Modified;
                    await _dbQuery.SaveChangesAsync();

                    //комитим все изменения
                    transaction.Complete();
                }
                catch (System.Exception ex)
                {
                    //отменяем все изменения и выбрасываем исключение
                    throw new Exception("ошибка обновления учредителя" + "\n" + ex.Message);
                }
            }
        }
        public async Task<Client?> ExistClient(int Id)
        {
            
            var client = await _dbCommand.clients.AsNoTracking().FirstOrDefaultAsync(c => c.Id == Id);
            return client;
        }
    }
}
