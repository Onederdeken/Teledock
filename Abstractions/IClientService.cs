using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teledock.Commands;
using Teledock.Models;
using Teledock.Queries;

namespace Teledock.Abstractions
{
    public interface IClientService
    {
        public Task<(String Error, ClientQuery client)> getClientById(int id);
        public Task<(String Error, List<ClientQuery> clients)> getAllClients();
        public Task<(String Error, List<ClientQuery> clients)> getULClients();
        public Task<(String Error, List<ClientQuery> clients)> getIPClients();
        public Task<(String Message, int code)> addClient(ClientCommand client);
        public Task<(String Message, int code)> UpdateClient(ClientCommand client, int clientID);
        public Task<(String Message, int code)> DeleteClient(int ClientId);
        
    }
}