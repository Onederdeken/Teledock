using MediatR;
using Teledock.Domain.Responses;

namespace Teledock.Application.Queries.Founders
{
    public class FounderQuery : IRequest<(string Error, FounderResponse FounderResponse)>
    {
        public int Id { get; set; }

        public string Inn { get; set; }

        public string FIO { get; set; }

        public string dateAdd { get; set; }

        public string? dateUpdate { get; set; }
    }
}