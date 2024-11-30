using MediatR;
using Teledock.Abstractions;
using Teledock.Queries.Founders;

namespace Teledock.MediatrHandlers.FounderHandlers
{
    public class FounderGetByIdQueryHandler : IRequestHandler<FounderQuery, (string Error, FounderQuery FounderQuery)>
    {
        private readonly IFounderService _founderService;
        public FounderGetByIdQueryHandler(IFounderService founderService)
        {

            _founderService = founderService;
        }
        public async Task<(string Error, FounderQuery FounderQuery)> Handle(FounderQuery request, CancellationToken cancellationToken)
        {
            return await _founderService.getFounderById(request.Id);
        }
    }
}
