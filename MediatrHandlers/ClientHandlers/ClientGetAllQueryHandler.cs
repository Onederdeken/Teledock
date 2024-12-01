﻿using MediatR;
using Teledock.Abstractions;
using Teledock.Mapper;
using Teledock.Models;
using Teledock.Queries.Clients;
using Teledock.Responses;

namespace Teledock.MediatrHandlers.ClientHandlers
{
    public class ClientGetAllQueryHandler : IRequestHandler<ClientsQueries, (String Error, List<ClientResponse> ClientResponse)>
    {
        private readonly IClientService _clientService;

        public ClientGetAllQueryHandler(IClientService clientService)
        {

            _clientService = clientService;
        }
        public async Task<(string Error, List<ClientResponse> ClientResponse)> Handle(ClientsQueries request, CancellationToken cancellationToken)
        {
            using(var mapper = new CustomMapper())
            {
                var result = request.Type switch
                {
                    TypeClient.UL => await _clientService.getULClients(),
                    TypeClient.IP => await _clientService.getIPClients(),
                    _ => await _clientService.getAllClients(),
                };
                if(result.Error == String.Empty) return (result.Error, mapper.MapToListClientResponse(result.clients));
                else return (result.Error, null);

            }
        }
    }
}
