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
        public FounderGetByIdQueryHandler(IFounderService founderService)
        {

            _founderService = founderService;
        }
        public async Task<(string Error, FounderResponse FounderResponse)> Handle(FounderQuery request, CancellationToken cancellationToken)
        {
            var result = await _founderService.getFounderById(request.Id);
            return result;
        }
    }
}
