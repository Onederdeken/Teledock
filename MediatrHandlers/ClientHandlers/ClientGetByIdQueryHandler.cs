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
        public ClientGetByIdQueryHandler(IClientService clientService)
        {

            _clientService = clientService;
        }
        public async Task<(string Error, ClientResponse ClientResponse)> Handle(ClientQuery request, CancellationToken cancellationToken)
        {
            using(var mapper = new CustomMapper())
            {
                var result = await _clientService.getClientById(request.Id);
                if(result.Error == String.Empty) return (result.Error, mapper.MapToClientResponse(result.client));
                else return (result.Error,null);
               
            }
        }
    }
}
