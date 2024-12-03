using MediatR;
using Teledock.Abstractions;
using Teledock.Domain.Enums;
using Teledock.Queries.Clients;
using Teledock.Responses;

namespace Teledock.MediatrHandlers.ClientHandlers
{
    public class ClientGetAllQueryHandler : IRequestHandler<ClientsQueries, (String Error, List<ClientResponse> ClientResponse)>
    {
        private readonly IClientService _clientService;
        private readonly ICustomMapper _customMapper;

        public ClientGetAllQueryHandler(IClientService clientService, ICustomMapper customMapper)
        {
            _clientService = clientService;
            _customMapper = customMapper;
        }
        public async Task<(string Error, List<ClientResponse> ClientResponse)> Handle(ClientsQueries request, CancellationToken cancellationToken)
        {
            
            var result = request.Type switch
            {
                TypeClient.UL => await _clientService.getULClients(),
                TypeClient.IP => await _clientService.getIPClients(),
                _ => await _clientService.getAllClients(),
            };
            var ClientResponse = _customMapper.MapToListClientResponse(result.clients);
            return (result.Error, ClientResponse);
        }
    }
}
