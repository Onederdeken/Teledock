using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teledock.Commands;
using Teledock.Models;
using Teledock.Queries.Clients;
using Teledock.Responses;

namespace Teledock.Abstractions
{
    public interface IClientService
    {
        public Task<(String Error, ClientResponse client)> getClientById(int id);
        public Task<(String Error, List<ClientResponse> clients)> getAllClients();
        public Task<(String Error, List<ClientResponse> clients)> getULClients();
        public Task<(String Error, List<ClientResponse> clients)> getIPClients();
        public Task<(String Message, int code)> addClient(ClientCommand client);
        public Task<(String Message, int code)> UpdateClient(ClientCommand client, int clientID);
        public Task<(String Message, int code)> DeleteClient(int ClientId);
        
    }
}