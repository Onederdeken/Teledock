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
            using(var mapper = new CustomMapper())
            {
                var result = await _founderService.getFounderById(request.Id);
                if(result.Error == String.Empty) return (result.Error, mapper.MapToFounderResponse(result.Founder));
                else return (result.Error, null);

            }
            
        }
    }
}
