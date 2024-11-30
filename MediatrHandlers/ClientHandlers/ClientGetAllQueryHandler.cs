using MediatR;
using Teledock.Abstractions;
using Teledock.Models;
using Teledock.Queries.Clients;

namespace Teledock.MediatrHandlers.ClientHandlers
{
    public class ClientGetAllQueryHandler : IRequestHandler<ClientsQueries, (String Error, List<ClientQuery> ClientQueries)>
    {
        private readonly IClientService _clientService;
        public ClientGetAllQueryHandler(IClientService clientService)
        {

            _clientService = clientService;
        }
        public async Task<(string Error, List<ClientQuery> ClientQueries)> Handle(ClientsQueries request, CancellationToken cancellationToken)
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
