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
            catch (System.Exception)
            {
               throw new Exception("ошибка получения списка клиентов");
            }
        }
        public async Task<List<Client>> getULClient(){
            try
            {
                return await _db.clients.Include(c=>c.founders).Where(c=>c._TypeClient == TypeClient.UL).ToListAsync();
            }
            catch (System.Exception)
            {
                throw new Exception("ошибка получения списка клиентов(юридические лица)");
            }
        }
        public async Task<List<Client>> getIPClient(){
            try
            {
                return await _db.clients.Where(c=>c._TypeClient == TypeClient.IP).ToListAsync();
            }
            catch (System.Exception)
            {
                throw new Exception("ошибка получения списка клиентов (индивидуальный предприниматель)");
            }
        }
        public async Task<Client?> getClientById(int Id){
            try
            {
                return await _db.clients.Include(c=>c.founders).FirstOrDefaultAsync(c=>c.Id == Id);
            }
            catch (System.Exception)
            {
                throw new Exception("ошибка получения клиента по id");
            }
        }
        public async Task AddIPClient(Client client){
            try
            {
                var add = _db.AddAsync(client);
                var save = _db.SaveChangesAsync();
                await add;
                await save;
            }
            catch (System.Exception)
            {
                throw new Exception("ошибка добавления клиента (индивидуальный предприниматель)");
            }
        }
        public async Task AddURClient(Client client, List<Founder> founders){
             using(var transaction = await _db.Database.BeginTransactionAsync()){
                try
                {
                    await _db.AddAsync(client);
                    await _db.SaveChangesAsync();
                    founders.ForEach( c=>{
                        c.ClientId = client.Id;
                        _db.AddAsync(c);
                    });
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("ошибка добавления клиента (юридическое лицо)");
                }
             }
        }
        public async Task AddFounder(int ClientId, Founder founder){
            try
            {
                founder.ClientId = ClientId;
                _db.Add(founder);
                await _db.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                
                throw new Exception("ошибка добавления учредителя для клиента");
            }
        }
        public async Task UpdateClient(int Id, Client client, List<Founder> founders){
            using(var transaction = await _db.Database.BeginTransactionAsync()){
                try
                {
                    _db.Attach(client);
                    _db.Entry(client).State = EntityState.Modified;
                    founders.ForEach(c=>{
                        _db.Attach(c);
                        _db.Entry(c).State = EntityState.Modified;
                    });
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();

                }
                catch (System.Exception)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("ошибка обновления клиента");
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
                catch (System.Exception)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("ошибка удаления клиента");
                }
            }
        }
        public async Task DeleteFounder(int FounderId){
            try
            {
                var founder = await _db.founders.FindAsync(FounderId);
                _db.founders.Remove(founder);
                await _db.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                throw new Exception("ошибка удаления учредителя");
            }
            
        }
        public void UpdateFounder(Founder founder){
            try
            {
                _db.Attach(founder);
                _db.Entry(founder).State = EntityState.Modified;
            }
            catch (System.Exception)
            {
                throw new Exception("ошибка обновления учредителя");
            }
        }
        public async Task<bool> ExistClient(int Id){
            var client = await _db.clients.FindAsync(Id);
            if(client == null)return false;
            else return true;
        }
        public async Task<bool> ExistFounder(int Id){
            var founder = await _db.founders.FindAsync(Id);
            if(founder == null)return false;
            else return true;
        }
    }
}