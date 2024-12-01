using MediatR;
using Teledock.Abstractions;
using Teledock.Mapper;
using Teledock.Queries.Clients;
using Teledock.Queries.Founders;
using Teledock.Responses;

namespace Teledock.MediatrHandlers.FounderHandlers
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
            var result =  await _founderService.getAllFounders();
            return result;
        }
    }
}
