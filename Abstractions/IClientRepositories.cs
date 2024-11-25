using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teledock.Models;

namespace Teledock.Abstractions
{
    public interface IClientRepositories
    {
        public  Task<List<Client>> getAllClients();
        public  Task<List<Client>> getULClient();
        public  Task<List<Client>> getIPClient();
        public  Task<Client> getClientById(int Id);
        public  Task AddIPClient(Client client);
        public  Task AddURClient(Client client, List<Founder> founders);
        public  Task UpdateClient(Client client, List<Founder> founders);
        public  Task DeleteClient(int Id);
        public Task UpdateFounder(Founder founder);
        public Task DeleteFounder(int FounderId);
        public Task AddFounder(int ClientId, Founder founder);
        public Task<bool> ExistClient(int Id);
        public Task<bool> ExistFounder(int Id);

    }
}