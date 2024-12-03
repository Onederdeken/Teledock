
using Teledock.Domain.Models;
using Teledock.Domain.Responses;

namespace Teledock.Domain.Abstractions
{
    public interface IClientService
    {
        public Task<(string Error, ClientResponse client)> getClientById(int id);
        public Task<(string Error, List<ClientResponse> clients)> getAllClients();
        public Task<(string Error, List<ClientResponse> clients)> getULClients();
        public Task<(string Error, List<ClientResponse> clients)> getIPClients();
        public Task<(string Message, int code)> addClient(Client client);
        public Task<(string Message, int code)> UpdateClient(Client client);
        public Task<(string Message, int code)> DeleteClient(int ClientId);

    }
}