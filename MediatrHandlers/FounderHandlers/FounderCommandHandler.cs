using MediatR;
using Teledock.Abstractions;
using Teledock.Commands;

namespace Teledock.MediatrHandlers.FounderHandlers
{
    public class FounderCommandHandler : IRequestHandler<FounderCommand, (string Message, int code)>
    {
        private readonly IFounderService _founderService;
        public FounderCommandHandler(IFounderService founderService)
        {

            _founderService = founderService;
        }
        public async Task<(string Message, int code)> Handle(FounderCommand request, CancellationToken cancellationToken)
        {
            var result = request.Command switch
            {
                Command.Add => await _founderService.AddFounder(request, (int)request.ClientId),
                Command.Update => await _founderService.UpdateFounder(request, (int)request.Id),
                Command.Delete => await _founderService.DeleteFounder((int)request.Id),
                Command.ChangeClient => await _founderService.ChangeClient((int)request.Id, (int)request.ClientId)
            };
            return result;
        }
    }
}
