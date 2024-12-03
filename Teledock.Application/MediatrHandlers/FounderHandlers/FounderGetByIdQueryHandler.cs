using MediatR;
using Teledock.Abstractions;
using Teledock.Mapper;
using Teledock.Queries.Founders;
using Teledock.Responses;

namespace Teledock.MediatrHandlers.FounderHandlers
{
    public class FounderGetByIdQueryHandler : IRequestHandler<FounderQuery, (string Error, FounderResponse FounderResponse)>
    {
        private readonly IFounderService _founderService;
        private readonly ICustomMapper _customMapper;
        public FounderGetByIdQueryHandler(IFounderService founderService, ICustomMapper customMapper)
        {
            _founderService = founderService;
            _customMapper = customMapper;
        }
        public async Task<(string Error, FounderResponse FounderResponse)> Handle(FounderQuery request, CancellationToken cancellationToken)
        {
            var result = await _founderService.getFounderById(request.Id);
            var FounderResponse = _customMapper.MapToFounderResponse(result.Founder);
            return (result.Error, FounderResponse);
        }
    }
}
