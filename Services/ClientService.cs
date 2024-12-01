using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teledock.Abstractions;
using Teledock.Commands;
using Teledock.Mapper;
using Teledock.Models;
using Teledock.Queries.Clients;
using Teledock.Queries.Founders;
using Teledock.Responses;

namespace Teledock.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepositories _ClientRep;
        public ClientService(IClientRepositories clientRepositories){
            this._ClientRep = clientRepositories;
        }

        public async Task<(string Message, int code)> addClient(ClientCommand client)
        {
            String message = String.Empty;
            int code = 0;
            try
            {
                using(var mapper = new CustomMapper())
                {
                    await _ClientRep.AddClient(mapper.MapToClient(client));
                    message = "Добавление прошло успешно";
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


        public async Task<(string Message, int code)> DeleteClient(int ClientId)
        {
            String message = String.Empty;
            int code = 0;
            Task result = null;
            try
            {
                if (await _ClientRep.ExistClient(ClientId))
                {
                    result = _ClientRep.DeleteClient(ClientId);
                    message = "Удаление прошло успешно";
                    code = 200;
                }
                else
                {
                    message = "такого клиента не существует";
                    code = 400;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                code = 400;
            }
            if (result != null) await result;
            return (message, code);

        }

        

        public async Task<(string Error, List<ClientResponse> clients)> getAllClients()
        {
            String Error = String.Empty;
            List<ClientQuery> clientQueries = null;
            List<ClientResponse> ClientResponse = new List<ClientResponse>();
            try
            {
                var clients = await _ClientRep.getAllClients();
                using(var mapper = new CustomMapper())
                {
                    clientQueries = mapper.MapToListClientQuery(clients);
                    ClientResponse = mapper.MapToListClientResponse(clientQueries);
                }
                
            }
            catch (System.Exception ex)
            {
                Error = ex.Message;
            }
            return (Error, ClientResponse);
        }

        public async Task<(string Error, ClientResponse? client)> getClientById(int id)
        {
            String Error = String.Empty;
            ClientQuery clientQuery = new ClientQuery();
            ClientResponse clientResponse = null;
            try
            {
                if(!await _ClientRep.ExistClient(id))return ("такого пользователя не существует",null);
                var client = await _ClientRep.getClientById(id);
                using(var mapper = new CustomMapper())
                {
                    clientQuery = mapper.MapToClientQuery(client);
                    clientResponse = mapper.MapToClientResponse(clientQuery);
                }
            }
            catch(Exception ex)
            {
                Error = ex.Message;
            }
            return (Error, clientResponse);
        }

        public async Task<(string Error, List<ClientResponse> clients)> getIPClients()
        {
            String Error = String.Empty;
            List<ClientQuery> clientQueries = new List<ClientQuery>();
            List<ClientResponse> ClientResponse = new List<ClientResponse>();
            try
            {
                var clients = await _ClientRep.getIPClient();
                using (var mapper = new CustomMapper())
                {
                    clientQueries = mapper.MapToListClientQuery(clients);
                    ClientResponse = mapper.MapToListClientResponse(clientQueries);
                }
                
            }
            catch(Exception ex)
            {
                Error = ex.Message;
            }
            return (Error, ClientResponse);
        }

        public async Task<(string Error, List<ClientResponse> clients)> getULClients()
        {
            String Error = String.Empty;
            List<ClientQuery> clientQueries = new List<ClientQuery>();
            List<ClientResponse> ClientResponse = new List<ClientResponse>();
            try
            {
                var clients = await _ClientRep.getULClient();
                using (var mapper = new CustomMapper())
                {
                    clientQueries = mapper.MapToListClientQuery(clients);
                    ClientResponse = mapper.MapToListClientResponse(clientQueries);
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return (Error, ClientResponse);
        }

        public async Task<(string Message, int code)> UpdateClient(ClientCommand Client, int clientID)
        {
            String message = String.Empty;
            int code = 0;
            try
            {
                if (!await _ClientRep.ExistClient(clientID)) return ("такого клиента не существует", 400);
                using (var mapper = new CustomMapper())
                {
                    var client = mapper.MapToClient(Client);
                    client.Id = clientID;
                    
                    await _ClientRep.UpdateClient(client);
                }
                message = "обновление прошло удачно";
                code = 200;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                code = 400;
            }
            return (message, code);
        }
    }
}