﻿using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using Teledock.Abstractions;
using Teledock.dbContext;
using Teledock.Models;
namespace Teledock.Repositories
{
    public class FounderRepositories : IFounderRepository
    {
        private readonly Db _db;
        public FounderRepositories(Db db)
        {
            this._db = db;
        }
        public async Task AddFounder(int ClientId, Founder founder)
        {

            try
            {
                var client = await _db.clients.FindAsync(ClientId);
                if(client._TypeClient == TypeClient.UL)
                {
                    founder.ClientId = ClientId;
                    _db.Add(founder);
                    await _db.SaveChangesAsync();
                }
                else {
                    throw new Exception("Вы пытаетесь добавить учредителя к ИП");
                }
               
            }
            catch (System.Exception ex)
            {

                throw new Exception("ошибка добавления учредителя для клиента" + "\n" + ex.Message);
            }
        }

        public async Task ChangeClient(int FounderId, int ClientId)
        {
            try
            {
                var Founder = _db.founders.Find(FounderId);
                Founder.ClientId = ClientId;
                await _db.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                throw new Exception("ошибка смены клиента" + "\n" + ex.Message);
            }
        }

        public async Task DeleteFounder(int FounderId)
        {
            try
            {
                var founder = await _db.founders.FindAsync(FounderId);
                _db.founders.Remove(founder);
                await _db.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                throw new Exception("ошибка удаления учредителя" +"\n" + ex.Message);
            }

        }
        public async Task<bool> ExistFounder(int FounderId)
        {
            var founder = await _db.founders.AsNoTracking().FirstOrDefaultAsync(c=>c.Id == FounderId);
            return founder == null ? false : true;
        }

        public async Task<List<Founder>> getAllFounders()
        {
            try
            {
                return await _db.founders.ToListAsync();
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
                return await _db.founders.FirstOrDefaultAsync(c => c.Id == FounderId);
            }
            catch (System.Exception ex)
            {
                throw new Exception("ошибка получения учредителя по id" + "\n" + ex.Message);
            }
        }

        public async Task UpdateFounder(Founder founder)
        {
            try
            {
                var Founder = _db.founders.Find(founder.Id);
                Founder.FIO = founder.FIO;
                Founder.Inn = founder.Inn;
                await _db.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                throw new Exception("ошибка обновления учредителя" + "\n" + ex.Message);
            }
        }
        public async Task<bool> ExistClient(int Id)
        {
            bool result;
            var client = await _db.clients.AsNoTracking().FirstOrDefaultAsync(c => c.Id == Id);
            return client == null ? false : true;
        }
    }
}