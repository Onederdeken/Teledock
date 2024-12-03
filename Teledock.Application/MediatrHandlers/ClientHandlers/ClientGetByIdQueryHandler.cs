using MediatR;
using Teledock.Abstractions;
using Teledock.Mapper;
using Teledock.Queries.Clients;
using Teledock.Responses;

namespace Teledock.MediatrHandlers.ClientHandlers
{
    public class ClientGetByIdQueryHandler : IRequestHandler<ClientQuery, (String Error, ClientResponse ClientResponse)>
    {
        private readonly IClientService _clientService;
        private readonly ICustomMapper _customMapper;
        public ClientGetByIdQueryHandler(IClientService clientService, ICustomMapper customMapper)
        {
            _clientService = clientService;
            _customMapper = customMapper;
        }
        public async Task<(string Error, ClientResponse ClientResponse)> Handle(ClientQuery request, CancellationToken cancellationToken)
        {
            var result = await _clientService.getClientById(request.Id);
            var ClientResponse = _customMapper.MapToClientResponse(result.client);
            return (result.Error, ClientResponse);

        }
    }
}
