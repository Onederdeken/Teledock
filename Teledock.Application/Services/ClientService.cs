
using Teledock.Domain.Abstractions;
using Teledock.Application.Commands;
using Teledock.Application.Queries.Clients;
using Teledock.Application.Mapper;

using Teledock.Domain.Responses;
using Teledock.Domain.Models;

namespace Teledock.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepositories _ClientRep;
        public ClientService(IClientRepositories clientRepositories)
        {
            _ClientRep = clientRepositories;
        }

        public async Task<(string Message, int code)> addClient(Client client)
        {
            string message = string.Empty;
            int code = 0;
            try
            {
                await _ClientRep.AddClient(client);
                message = "Добавление прошло успешно";
                code = 200;
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
            string message = string.Empty;
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
            string Error = string.Empty;
            List<ClientQuery> clientQueries = null;
            List<ClientResponse> ClientResponse = new List<ClientResponse>();
            try
            {
                var clients = await _ClientRep.getAllClients();
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

        public async Task<(string Error, ClientResponse? client)> getClientById(int id)
        {
            string Error = string.Empty;
            ClientQuery clientQuery = new ClientQuery();
            ClientResponse clientResponse = null;
            try
            {
                if (!await _ClientRep.ExistClient(id)) return ("такого клиента не существует", null);
                var client = await _ClientRep.getClientById(id);
                using (var mapper = new CustomMapper())
                {
                    clientQuery = mapper.MapToClientQuery(client);
                    clientResponse = mapper.MapToClientResponse(clientQuery);
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return (Error, clientResponse);
        }

        public async Task<(string Error, List<ClientResponse> clients)> getIPClients()
        {
            string Error = string.Empty;
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
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return (Error, ClientResponse);
        }

        public async Task<(string Error, List<ClientResponse> clients)> getULClients()
        {
            string Error = string.Empty;
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

        public async Task<(string Message, int code)> UpdateClient(Client Client)
        {
            string message = string.Empty;
            int code = 0;
            try
            {
                if (!await _ClientRep.ExistClient(Client.Id)) return ("такого клиента не существует", 400);
                await _ClientRep.UpdateClient(Client);
              
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