using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Teledock.Abstractions;
using Teledock.dbContext;
using Teledock.Models;

namespace Teledock.Repositories
{
    public class ClientRepositories:IClientRepositories
    {
        private readonly Db _db;
        public ClientRepositories(Db db){
            this._db = db;
        }
        public async Task<List<Client>> getAllClients(){
            try
            {
                return await _db.clients.Include(c=>c.founders).ToListAsync();
            }
            catch (System.Exception ex)
            {
               throw new Exception($"ошибка получения списка клиентов + \n ошибка:{ex.Message}" + "\n" +"внутреняя ошибка:" + ex.InnerException.Message);
            }
        }
        public async Task<List<Client>> getULClient(){
            try
            {
                return await _db.clients.Include(c=>c.founders).Where(c=>c._TypeClient == TypeClient.UL).ToListAsync();
            }
            catch (System.Exception ex)
            {
                throw new Exception("ошибка получения списка клиентов(юридические лица)" + "\nОшибка: " + ex.Message + "\n" + "внутреняя ошибка:" + ex.InnerException.Message);
            }
        }
        public async Task<List<Client>> getIPClient(){
            try
            {
                return await _db.clients.Where(c=>c._TypeClient == TypeClient.IP).ToListAsync();
            }
            catch (System.Exception ex)
            {
                throw new Exception("ошибка получения списка клиентов (индивидуальный предприниматель)" + "\nОшибка: " + ex.Message + "\n" + "внутреняя ошибка:" + ex.InnerException.Message);
            }
        }
        public async Task<Client?> getClientById(int Id){
            try
            {
                return await _db.clients.Include(c=>c.founders).FirstOrDefaultAsync(c=>c.Id == Id);
            }
            catch (System.Exception ex)
            {
                throw new Exception("ошибка получения клиента по id" + "\nОшибка: " + ex.Message + "\n" + "внутреняя ошибка:" + ex.InnerException.Message);
            }
        }
        public async Task AddClient(Client client){
            try
            {
                var add = _db.AddAsync(client);
                var save = _db.SaveChangesAsync();
                await add;
                await save;
            }
            catch (System.Exception ex)
            {
                throw new Exception("ошибка добавления клиента"+ "\nОшибка: " + ex.Message + "\n" + "внутреняя ошибка:" + ex.InnerException.Message);
            }
        }
       
        public async Task UpdateClient(Client client){
            using(var transaction = await _db.Database.BeginTransactionAsync()){
                try
                {
                    
                    var Client = await _db.clients.FindAsync(client.Id);
                    Client.Name = client.Name;
                    Client.Inn = client.Inn;
                    if (client._TypeClient != 0) Client._TypeClient = client._TypeClient;
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();

                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("ошибка обновления клиента" + "\nОшибка: " + ex.Message + "\n" + "внутреняя ошибка:" + ex.InnerException.Message);
                }
            }

        }
        public async Task DeleteClient(int Id){
            using(var transaction = await _db.Database.BeginTransactionAsync()){
                try
                {
                    var client = await _db.clients.Include(c=>c.founders).FirstOrDefaultAsync(c=>c.Id == Id);
                    _db.founders.RemoveRange(client.founders);
                    _db.clients.Remove(client);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("ошибка удаления клиента" + "\nОшибка: " + ex.Message + "\n" + "внутреняя ошибка:" + ex.InnerException.Message);
                }
            }
        }
        public async Task<bool> ExistClient(int Id){
            bool result;
            var client = await _db.clients.AsNoTracking().FirstOrDefaultAsync(c=>c.Id == Id);
            return client == null?false : true;
        }
        
    }
}