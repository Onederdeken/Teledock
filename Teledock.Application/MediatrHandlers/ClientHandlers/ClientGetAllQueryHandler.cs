using MediatR;
using Teledock.Domain.Abstractions;
using Teledock.Application.Queries.Clients;
using Teledock.Domain.Responses;
using Teledock.Domain.Enums;

namespace Teledock.Application.MediatrHandlers.ClientHandlers
{
    public class ClientGetAllQueryHandler : IRequestHandler<ClientsQueries, (string Error, List<ClientResponse> ClientResponse)>
    {
        private readonly IClientService _clientService;

        public ClientGetAllQueryHandler(IClientService clientService)
        {

            _clientService = clientService;
        }
        public async Task<(string Error, List<ClientResponse> ClientResponse)> Handle(ClientsQueries request, CancellationToken cancellationToken)
        {

            var result = request.Type switch
            {
                TypeClient.UL => await _clientService.getULClients(),
                TypeClient.IP => await _clientService.getIPClients(),
                _ => await _clientService.getAllClients(),
            };
            return result;
        }
    }
}
