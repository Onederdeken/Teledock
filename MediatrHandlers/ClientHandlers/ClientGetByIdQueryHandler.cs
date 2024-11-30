using MediatR;
using Teledock.Abstractions;
using Teledock.Queries.Clients;

namespace Teledock.MediatrHandlers.ClientHandlers
{
    public class ClientGetByIdQueryHandler : IRequestHandler<ClientQuery, (String Error, ClientQuery ClientQuery)>
    {
        private readonly IClientService _clientService;
        public ClientGetByIdQueryHandler(IClientService clientService)
        {

            _clientService = clientService;
        }
        public async Task<(string Error, ClientQuery ClientQuery)> Handle(ClientQuery request, CancellationToken cancellationToken)
        {
            return await _clientService.getClientById(request.Id);
        }
    }
}
