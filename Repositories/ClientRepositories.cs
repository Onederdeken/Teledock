using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Teledock.Abstractions;
using Teledock.dbContext;
using Teledock.Models;

namespace Teledock.Repositories
{
    public class ClientRepositories:IClientRepositories
    {
        private readonly DbCommand _dbCommand;
        private readonly DbQuery _dbQuery;
        public ClientRepositories(DbCommand db, DbQuery dbQuery){
            this._dbCommand = db;
            this._dbQuery = dbQuery;
        }
        public async Task<List<Client>> getAllClients(){
            try
            {
                return await _dbQuery.clients.Include(c=>c.founders).ToListAsync();
            }
            catch (System.Exception ex)
            {
               throw new Exception($"ошибка получения списка клиентов + \n ошибка:{ex.Message}" + "\n" +"внутреняя ошибка:" + ex.InnerException.Message);
            }
        }
        public async Task<List<Client>> getULClient(){
            try
            {
                return await _dbQuery.clients.Include(c=>c.founders).Where(c=>c._TypeClient == TypeClient.UL).ToListAsync();
            }
            catch (System.Exception ex)
            {
                throw new Exception("ошибка получения списка клиентов(юридические лица)" + "\nОшибка: " + ex.Message + "\n" + "внутреняя ошибка:" + ex.InnerException.Message);
            }
        }
        public async Task<List<Client>> getIPClient(){
            try
            {
                return await _dbQuery.clients.Where(c=>c._TypeClient == TypeClient.IP).ToListAsync();
            }
            catch (System.Exception ex)
            {
                throw new Exception("ошибка получения списка клиентов (индивидуальный предприниматель)" + "\nОшибка: " + ex.Message + "\n" + "внутреняя ошибка:" + ex.InnerException.Message);
            }
        }
        public async Task<Client?> getClientById(int Id){
            try
            {
                return await _dbQuery.clients.Include(c=>c.founders).FirstOrDefaultAsync(c=>c.Id == Id);
            }
            catch (System.Exception ex)
            {
                throw new Exception("ошибка получения клиента по id" + "\nОшибка: " + ex.Message + "\n" + "внутреняя ошибка:" + ex.InnerException.Message);
            }
        }
        public async Task AddClient(Client client){
            using(var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    //добавляем в базу даннхы dbCommand
                    await _dbCommand.AddAsync(client);
                    await _dbCommand.SaveChangesAsync();

                    //вносим изменения в базу данных dbQuery дабы сохранить целостность
                    await _dbQuery.AddAsync(client);
                    await _dbQuery.SaveChangesAsync();

                    //если все прошло хорошо комитим изменения
                    transaction.Complete();
                }
                catch (System.Exception ex)
                {
                    //если нет отменяем все изменения и выбрасываем исключение
                    throw new Exception("ошибка добавления клиента" + "\nОшибка: " + ex.Message + "\n" + "внутреняя ошибка:" + ex.InnerException.Message);
                }
            }
        }
       
        public async Task UpdateClient(Client client){
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    //меняем поля одной записи
                    var Client = await _dbCommand.clients.FindAsync(client.Id);
                    Client.Name = client.Name;
                    Client.Inn = client.Inn;
                    if (client._TypeClient != 0) Client._TypeClient = client._TypeClient; // если с контроллера прилетел _TypeClient == 0, то
                    //сохраняем изменения в бд dbCommand                                     тип не меняется иначе мы его меняем
                    await _dbCommand.SaveChangesAsync();

                    //вносим изменения в базу данных dbQuery дабы сохранить целостность
                    _dbQuery.Entry(Client).State = EntityState.Modified;
                    await _dbQuery.SaveChangesAsync();
                    // комитим изменения
                    transaction.Complete();
                }
                catch (System.Exception ex)
                {
                    //отменяем изменения и выбрасываем исключение
                    throw new Exception("ошибка обновления клиента" + "\nОшибка: " + ex.Message + "\n" + "внутреняя ошибка:" + ex.InnerException == null? " ": ex.InnerException.Message);
                }
            }
        }
        public async Task DeleteClient(int Id){
            using(var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    //находим запись клиента по Id
                    var client = await _dbCommand.clients.Include(c=>c.founders).FirstOrDefaultAsync(c=>c.Id == Id);
                    //удаляем все зависимые сущности
                    _dbCommand.founders.RemoveRange(client.founders);
                    //удалаяем саму запись
                    _dbCommand.clients.Remove(client);
                    //сохраняем изменения
                    await _dbCommand.SaveChangesAsync();

                    //вносим изменения в базу данных dbQuery дабы сохранить целостность
                    _dbQuery.Attach(client);
                    _dbQuery.founders.RemoveRange(client.founders);
                    _dbQuery.clients.Remove(client);
                    await _dbQuery.SaveChangesAsync();

                    //коммитим изменения в случае удачи
                    transaction.Complete();
                }
                catch (System.Exception ex)
                {
                    //отменяем изменения и выбрасываем исключение
                    throw new Exception("ошибка удаления клиента" + "\nОшибка: " + ex.Message + "\n" + "внутреняя ошибка:" + ex.InnerException.Message);
                }
            }
        }
        public async Task<bool> ExistClient(int Id){
            bool result;
            var client = await _dbCommand.clients.AsNoTracking().FirstOrDefaultAsync(c=>c.Id == Id);
            return client == null?false : true;
        }
        
    }
}