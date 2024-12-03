using Teledock.Abstractions;
using Teledock.Commands;
using Teledock.Domain.Models;
using Teledock.Mapper;
using Teledock.Queries.Clients;
using Teledock.Responses;

namespace Teledock.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepositories _ClientRep;
        public ClientService(IClientRepositories clientRepositories){
            this._ClientRep = clientRepositories;
        }

        public async Task<(string Message, int code)> addClient(Client client)
        {
            String message = String.Empty;
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

        

        public async Task<(string Error, List<Client> clients)> getAllClients()
        {
            String Error = String.Empty;
            List<Client> clients = null;
            try
            {
                clients = await _ClientRep.getAllClients();
                
            }
            catch (System.Exception ex)
            {
                Error = ex.Message;
            }
            return (Error, clients);
        }

        public async Task<(string Error, Client? client)> getClientById(int id)
        {
            String Error = String.Empty;
            ClientQuery clientQuery = new ClientQuery();
            ClientResponse clientResponse = null;
            Client? client = null;
            try
            {
                if(!await _ClientRep.ExistClient(id))return ("такого клиента не существует",null);
                client = await _ClientRep.getClientById(id);
            }
            catch(Exception ex)
            {
                Error = ex.Message;
            }
            return (Error, client);
        }

        public async Task<(string Error, List<Client> clients)> getIPClients()
        {
            String Error = String.Empty;
            List<Client> clients = null;
            try
            {
                clients = await _ClientRep.getIPClient();
            }
            catch(Exception ex)
            {
                Error = ex.Message;
            }
            return (Error,clients);
        }

        public async Task<(string Error, List<Client> clients)> getULClients()
        {
            String Error = String.Empty;
            List<Client> clients = null;
            try
            {
                clients = await _ClientRep.getULClient();
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return (Error, clients);
        }

        public async Task<(string Message, int code)> UpdateClient(Client Client)
        {
            String message = String.Empty;
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