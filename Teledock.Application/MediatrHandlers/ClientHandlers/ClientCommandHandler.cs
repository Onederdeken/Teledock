using MediatR;
using Teledock.Application.Commands;
using Teledock.Domain.Abstractions;
using Teledock.Domain.Enums;
using Teledock.Domain.Models;

namespace Teledock.Application.MediatrHandlers.ClientHandlers
{
    public class ClientCommandHandler : IRequestHandler<ClientCommand, (string Message, int code)>
    {
        private readonly IClientService _clientService;
        private readonly ICustomMapper _customMapper;
        public ClientCommandHandler(IClientService clientService, ICustomMapper mapper)
        {
            _clientService = clientService;
            _customMapper = mapper;
        }
        public async Task<(string Message, int code)> Handle(ClientCommand request, CancellationToken cancellationToken)
        {
            Client client = _customMapper.MapToClient(request); 
            var result = request.comand switch
            {
                Command.Update => await _clientService.UpdateClient(client),
                Command.Add => await _clientService.addClient(client),
                Command.Delete => await _clientService.DeleteClient(client.Id)
            };
            return result;
        }
    }
}
