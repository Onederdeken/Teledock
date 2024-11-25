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
            try
            {
                using(var mapper = new CustomMapper())
                {
                    await _ClientRep.AddIPClient(mapper.MapToClient(client));
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

        public async Task<(string Message, int code)> addClientUL(ClientULCommand client, List<FounderCommand> founders)
        {
            String message = String.Empty;
            int code = 0;
            try
            {
                using(var mapper = new CustomMapper())
                {
                    await _ClientRep.AddURClient(mapper.MapToClient(client), mapper.MapToListFounder(founders));
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

        public async Task<(string Message, int code)> DeleteFounder(int FounderId)
        {
            String Message = String.Empty;
            int code = 0;
            Task result = null;
            try
            {
                if (await _ClientRep.ExistFounder(FounderId))
                {
                    result = _ClientRep.DeleteFounder(FounderId);
                    Message = "удаление прошло успешно";
                    code = 200;
                }
                else
                {
                    Message = "такого учредителя не существует";
                    code = 400;
                }
            }
            catch(Exception ex)
            {
                Message = ex.Message;
                code = 400;
            }
            return (Message, code);
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

        public async Task<(string Error, ClientQuery client)> getClientById(int id)
        {
            String Error = String.Empty;
            ClientQuery clientQuery = new ClientQuery();
            try
            {
                var client = await _ClientRep.getClientById(id);
                using(var mapper = new CustomMapper())
                {
                    clientQuery = mapper.MapToClientQuery(client);
                }
            }
            catch(Exception ex)
            {
                Error = ex.Message;
            }
            return (Error, clientQuery);
        }

        public async Task<(string Error, List<ClientQuery> clients)> getIPClients()
        {
            String Error = String.Empty;
            List<ClientQuery> clientQueries = new List<ClientQuery>();
            try
            {
                var clients = await _ClientRep.getIPClient();
                using (var mapper = new CustomMapper())
                {
                    clientQueries = mapper.MapToListClientQuery(clients);
                }
                
            }
            catch(Exception ex)
            {
                Error = ex.Message;
            }
            return (Error, clientQueries);
        }

        public async Task<(string Error, List<ClientQuery> clients)> getULClients()
        {
            String Error = String.Empty;
            List<ClientQuery> clientQueries = new List<ClientQuery>();
            try
            {
                var clients = await _ClientRep.getULClient();
                using (var mapper = new CustomMapper())
                {
                    clientQueries = mapper.MapToListClientQuery(clients);
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return (Error, clientQueries);
        }

        public async Task<(string Message, int code)> UpdateClient(ClientIPCommand clientIP)
        {
            String message = String.Empty;
            int code = 0;
            try
            {
                using(var mapper = new CustomMapper())
                {
                    var client = mapper.MapToClient(clientIP);
                    await _ClientRep.UpdateClient(client,new List<Founder>());
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
        public async Task<(string Message, int code)> UpdateClient(ClientULCommand clientUL, List<FounderCommand> foundersCommand)
        {
            String message = String.Empty;
            int code = 0;
            try
            {
                using (var mapper = new CustomMapper())
                {
                    var client = mapper.MapToClient(clientUL);
                    var founders = mapper.MapToListFounder(foundersCommand);
                    await _ClientRep.UpdateClient(client,founders);
                    message = "обновление прошло удачно";
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

        public async Task<(string Message, int code)> UpdateFounder(FounderCommand founderCommand)
        {
            String message = String.Empty;
            int code = 0;
            try
            {
                using (var mapper = new CustomMapper())
                {
                    var founder = mapper.MapToFounder(founderCommand);
                   
                    await _ClientRep.UpdateFounder (founder);
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