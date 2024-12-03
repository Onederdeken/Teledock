using MediatR;
using Teledock.Application.Queries.Founders;
using Teledock.Domain.Abstractions;
using Teledock.Domain.Responses;


namespace Teledock.Application.MediatrHandlers.FounderHandlers
{
    public class FounderGetAllQueriesHandler : IRequestHandler<FoundersQueries, (string Error, List<FounderResponse> FounderResponse)>
    {
        private readonly IFounderService _founderService;
        public FounderGetAllQueriesHandler(IFounderService founderService)
        {

            _founderService = founderService;
        }
        public async Task<(string Error, List<FounderResponse> FounderResponse)> Handle(FoundersQueries request, CancellationToken cancellationToken)
        {
            var result = await _founderService.getAllFounders();
            return result;
        }
    }
}
