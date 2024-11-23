using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teledock.Abstractions;
using Teledock.Commands;
using Teledock.Models;
using Teledock.Queries;

namespace Teledock.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepositories _ClientRep;
        public ClientService(IClientRepositories clientRepositories){
            this._ClientRep = clientRepositories;
        }

        public Task<(string Message, int code)> addClientIP(ClientComand client)
        {
            throw new NotImplementedException();
        }

        public Task<(string Message, int code)> addClientUR(ClientComand client, List<FounderCommand> founders)
        {
            throw new NotImplementedException();
        }

        public async Task<(string Error, List<ClientQuery> clients)> getAllClients()
        {
            String Error = String.Empty;
            List<ClientQuery> clientQueries = null;
            List<FounderQuery> founderQueries = null;
            try
            {
                var clients = await _ClientRep.getAllClients();
                clientQueries = new List<ClientQuery>();
                
                clients.ForEach(c=>{
                    founderQueries = new List<FounderQuery>();
                    c.founders.ForEach(b=>{
                        var founderQuery = new FounderQuery(){
                            Inn = b.Inn,
                            FIO = b.FIO,
                            dateAdd = b.dateAdd,
                            dateUpdate = b.dateUpdate
                        };
                        founderQueries.Add(founderQuery);
                    });
                    clientQueries.Add(

                        new ClientQuery(){
                            Name = c.Name,
                            Inn = c.Inn,
                            dateAdd = c.dateAdd,
                            dateUpdate = c.dateUpdate,
                            Type = c._TypeClient.ToString(),
                            Queryfounders = founderQueries
                        }
                    );
                });
            }
            catch (System.Exception ex)
            {
                Error = ex.Message;
            }
            return (Error, clientQueries);
        }

        public Task<(string Error, ClientQuery client)> getClientById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<(string Error, List<ClientQuery> clients)> getIPClients()
        {
            throw new NotImplementedException();
        }

        public Task<(string Error, List<ClientQuery> clients)> getUrClients()
        {
            throw new NotImplementedException();
        }

        public Task<(string Message, int code)> UpdateClient(ClientComand client, List<FounderCommand> founders)
        {
            throw new NotImplementedException();
        }

        public Task<(string Message, int code)> UpdateFounder(FounderCommand founder)
        {
            throw new NotImplementedException();
        }
    }
}