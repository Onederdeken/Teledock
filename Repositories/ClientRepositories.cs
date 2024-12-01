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
                    await _dbCommand.AddAsync(client);
                    await _dbCommand.SaveChangesAsync();
                    transaction.Complete();
                }
                catch (System.Exception ex)
                {
                    throw new Exception("ошибка добавления клиента" + "\nОшибка: " + ex.Message + "\n" + "внутреняя ошибка:" + ex.InnerException.Message);
                }
            }
        }
       
        public async Task UpdateClient(Client client){
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var Client = await _dbCommand.clients.FindAsync(client.Id);
                    Client.Name = client.Name;
                    Client.Inn = client.Inn;
                    if (client._TypeClient != 0) Client._TypeClient = client._TypeClient;
                    await _dbCommand.SaveChangesAsync();
                    transaction.Complete();
                }
                catch (System.Exception ex)
                {

                    throw new Exception("ошибка обновления клиента" + "\nОшибка: " + ex.Message + "\n" + "внутреняя ошибка:" + ex.InnerException.Message);
                }
            }
        }
        public async Task DeleteClient(int Id){
            using(var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var client = await _dbCommand.clients.Include(c=>c.founders).FirstOrDefaultAsync(c=>c.Id == Id);
                    _dbCommand.founders.RemoveRange(client.founders);
                    _dbCommand.clients.Remove(client);
                    await _dbCommand.SaveChangesAsync();
                    transaction.Complete();
                }
                catch (System.Exception ex)
                {
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