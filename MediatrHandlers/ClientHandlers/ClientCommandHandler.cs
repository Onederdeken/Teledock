using MediatR;
using Teledock.Abstractions;
using Teledock.Commands;

namespace Teledock.MediatrHandlers.ClientHandlers
{
    public class ClientCommandHandler : IRequestHandler<ClientCommand, (string Message, int code)>
    {
        private readonly IClientService _clientService;
        public ClientCommandHandler(IClientService clientService)
        {

            _clientService = clientService;
        }
        public async Task<(string Message, int code)> Handle(ClientCommand request, CancellationToken cancellationToken)
        {
            var result = request.comand switch
            {
                Command.Update => await _clientService.UpdateClient(request, request.Id),
                Command.Add => await _clientService.addClient(request),
                Command.Delete => await _clientService.DeleteClient(request.Id)
            };
            return result;
        }
    }
}
