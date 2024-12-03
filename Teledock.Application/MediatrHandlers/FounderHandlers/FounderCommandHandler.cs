using MediatR;
using Teledock.Application.Commands;
using Teledock.Domain.Abstractions;
using Teledock.Domain.Enums;

namespace Teledock.Application.MediatrHandlers.FounderHandlers
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
                Command.Add => await _founderService.AddFounder(Founder, Founder.ClientId),
                Command.Update => await _founderService.UpdateFounder(Founder),
                Command.Delete => await _founderService.DeleteFounder(Founder.Id),
                Command.ChangeClient => await _founderService.ChangeClient(Founder.Id,Founder.ClientId)
            };
            return result;
        }
    }
}
