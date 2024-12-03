using Teledock.Domain.Models;
using Teledock.Responses;

namespace Teledock.Abstractions
{
    public interface IClientService
    {
        public Task<(String Error, Client client)> getClientById(int id);
        public Task<(String Error, List<Client> clients)> getAllClients();
        public Task<(String Error, List<Client> clients)> getULClients();
        public Task<(String Error, List<Client> clients)> getIPClients();
        public Task<(String Message, int code)> addClient(Client client);
        public Task<(String Message, int code)> UpdateClient(Client client);
        public Task<(String Message, int code)> DeleteClient(int ClientId);
        
    }
}