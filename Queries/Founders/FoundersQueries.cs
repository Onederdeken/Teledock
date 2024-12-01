using MediatR;
using Teledock.Responses;

namespace Teledock.Queries.Founders
{
    public class FoundersQueries : IRequest<(string Error, List<FounderResponse> FounderResponse)>
    {
        public int Id { get; set; }

        public string Inn { get; set; }

        public string FIO { get; set; }

        public string dateAdd { get; set; }

        public string? dateUpdate { get; set; }
    }
}
