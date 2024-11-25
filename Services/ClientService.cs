using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teledock.Abstractions;
using Teledock.Commands;
using Teledock.Mapper;
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

        public async Task<(string Message, int code)> addClientIP(ClientIPCommand client)
        {
            String message = String.Empty;
            int code = 0;
            
            if(client.getTypeClient() != TypeClient.UL && client.getTypeClient() != TypeClient.IP)
            {
                message = "не верно указан тип клиента";
                code = 400;
                return (message, code);
            }
            else if (client.getTypeClient() != TypeClient.IP)
            {
                message = "вы пытаетесь добаввить не тот тип клиента";
                code = 400;
                return (message, code);
            }
            try
            {
                using(var mapper = new CustomMapper())
                {
                    await _ClientRep.AddIPClient(mapper.MapToClient(client));
                    message = "ƒобавление прошло успешно";
                    code = 200;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                code = 400;
            }
            return (message, code);
        }

        public async Task<(string Message, int code)> addClientUL(ClientULCommand client, List<FounderCommand> founders)
        {
            String message = String.Empty;
            int code = 0;
            
            
            if (client.getTypeClient() != TypeClient.UL && client.getTypeClient() != TypeClient.IP)
            {
                message = "не верно указан тип клиента";
                code = 400;
                return (message, code);
            }
            else if (client.getTypeClient() != TypeClient.UL)
            {
                message = "вы пытаетесь добаввить не тот тип клиента";
                code = 400;
                return (message, code);
            }
            try
            {
                using(var mapper = new CustomMapper())
                {
                    await _ClientRep.AddURClient(mapper.MapToClient(client), mapper.MapToListFounder(founders));
                    message = "ƒобавление прошло успешно";
                    code = 200;
                }
               
            }
            catch (Exception ex)
            {
                message = ex.Message;
                code = 400;
            }
            return (message, code);
        }

        public async Task<(string Error, List<ClientQuery> clients)> getAllClients()
        {
            String Error = String.Empty;
            List<ClientQuery> clientQueries = null;
            List<FounderQuery> founderQueries = null;
            try
            {
                var clients = await _ClientRep.getAllClients();
                using(var mapper = new CustomMapper())
                {
                    clientQueries = mapper.MapToListClientQuery(clients);
                }
                
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

        public Task<(string Message, int code)> UpdateClient(ClientIPCommand client)
        {
            throw new NotImplementedException();
        }
        public Task<(string Message, int code)> UpdateClient(ClientULCommand client, List<FounderCommand> founders)
        {
            throw new NotImplementedException();
        }

        public Task<(string Message, int code)> UpdateFounder(FounderCommand founder)
        {
            throw new NotImplementedException();
        }
     
    }
}