using MediatR;
using Teledock.Application.Queries.Founders;
using Teledock.Domain.Abstractions;
using Teledock.Domain.Responses;

namespace Teledock.Application.MediatrHandlers.FounderHandlers
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
