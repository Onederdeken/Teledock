using MediatR;

namespace Teledock.Queries.Founders
{
    public class FoundersQueries : IRequest<(string Error, List<FounderQuery> FounderQuery)>
    {
        public int Id { get; set; }

        public string Inn { get; set; }

        public string FIO { get; set; }

        public string dateAdd { get; set; }

        public string? dateUpdate { get; set; }
    }
}
