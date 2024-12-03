using MediatR;
using Teledock.Abstractions;
using Teledock.Commands;
using Teledock.Domain.Enums;
using Teledock.Domain.Models;

namespace Teledock.MediatrHandlers.ClientHandlers
{
    public class ClientCommandHandler : IRequestHandler<ClientCommand, (string Message, int code)>
    {
        private readonly IClientService _clientService;
        private readonly ICustomMapper _customMapper;
        public ClientCommandHandler(IClientService clientService, ICustomMapper customMapper)
        {
            _clientService = clientService;
            _customMapper = customMapper;
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
