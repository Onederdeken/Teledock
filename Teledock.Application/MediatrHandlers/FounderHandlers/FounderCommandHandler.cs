using MediatR;
using Teledock.Abstractions;
using Teledock.Commands;
using Teledock.Domain.Enums;

namespace Teledock.MediatrHandlers.FounderHandlers
{
    public class FounderCommandHandler : IRequestHandler<FounderCommand, (string Message, int code)>
    {
        private readonly IFounderService _founderService;
        private readonly ICustomMapper _customMapper;
        public FounderCommandHandler(IFounderService founderService, ICustomMapper customMapper)
        {
            _founderService = founderService;
            _customMapper = customMapper;
        }
        public async Task<(string Message, int code)> Handle(FounderCommand request, CancellationToken cancellationToken)
        {
            var Founder = _customMapper.MapToFounder(request);
            var result = request.Command switch
            {
                Command.Add => await _founderService.AddFounder(Founder),
                Command.Update => await _founderService.UpdateFounder(Founder),
                Command.Delete => await _founderService.DeleteFounder((int)request.Id),
                Command.ChangeClient => await _founderService.ChangeClient((int)request.Id, (int)request.ClientId)
            };
            return result;
        }
    }
}
