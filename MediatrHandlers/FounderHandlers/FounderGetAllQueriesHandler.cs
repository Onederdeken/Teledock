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
            using(var mapper = new CustomMapper())
            {
                var result =  await _founderService.getAllFounders();
                if(result.Error == String.Empty) return (result.Error, mapper.MapToListFounderResponse(result.Founders));
                else return (result.Error, null);
            }


        }
    }
}
