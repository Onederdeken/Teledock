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
        public Task<(String Error, List<ClientQuery> clients)> getUrClients();
        public Task<(String Error, List<ClientQuery> clients)> getIPClients();
        public Task<(String Message, int code)> addClientUL(ClientULCommand client, List<FounderCommand> founders);
        public Task<(String Message, int code)> addClientIP(ClientIPCommand client);
        public Task<(String Message, int code)> UpdateClient(ClientIPCommand client);
        public Task<(String Message, int code)> UpdateClient(ClientULCommand client, List<FounderCommand> founders);
        public Task<(String Message, int code)> UpdateFounder(FounderCommand founder);
    }
}