using Teledock.Domain.Models;

namespace Teledock.Abstractions
{
    public interface IClientRepositories
    {
        public  Task<List<Client>> getAllClients();
        public  Task<List<Client>> getULClient();
        public  Task<List<Client>> getIPClient();
        public  Task<Client> getClientById(int Id);
        public  Task AddClient(Client client);
        public  Task UpdateClient(Client client);
        public  Task DeleteClient(int Id);

        public Task<bool> ExistClient(int Id);


    }
}