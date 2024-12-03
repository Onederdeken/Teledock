using MediatR;
using Teledock.Application.Queries.Clients;
using Teledock.Domain.Responses;
using Teledock.Domain.Abstractions;

namespace Teledock.Application.MediatrHandlers.ClientHandlers
{
    public class ClientGetByIdQueryHandler : IRequestHandler<ClientQuery, (string Error, ClientResponse ClientResponse)>
    {
        private readonly IClientService _clientService;
        public ClientGetByIdQueryHandler(IClientService clientService)
        {

            _clientService = clientService;
        }
        public async Task<(string Error, ClientResponse ClientResponse)> Handle(ClientQuery request, CancellationToken cancellationToken)
        {
            var result = await _clientService.getClientById(request.Id);
            return result;

        }
    }
}
