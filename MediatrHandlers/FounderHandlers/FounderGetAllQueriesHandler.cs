using MediatR;
using Teledock.Abstractions;
using Teledock.Queries.Clients;
using Teledock.Queries.Founders;

namespace Teledock.MediatrHandlers.FounderHandlers
{
    public class FounderGetAllQueriesHandler : IRequestHandler<FoundersQueries, (string Error, List<FounderQuery> FounderQuery)>
    {
        private readonly IFounderService _founderService;
        public FounderGetAllQueriesHandler(IFounderService founderService)
        {

            _founderService = founderService;
        }
        public async Task<(string Error, List<FounderQuery> FounderQuery)> Handle(FoundersQueries request, CancellationToken cancellationToken)
        {
            return await _founderService.getAllFounders();
        }
    }
}
