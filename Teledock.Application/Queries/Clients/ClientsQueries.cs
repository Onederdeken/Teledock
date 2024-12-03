using MediatR;
using Teledock.Application.Queries.Founders;
using Teledock.Domain.Enums;
using Teledock.Domain.Responses;

namespace Teledock.Application.Queries.Clients
{
    public class ClientsQueries : IRequest<(string Error, List<ClientResponse> ClientQueries)>
    {
        public int Id { get; set; }
        public string Inn { get; set; }
        public string Name { get; set; }
        public TypeClient Type { get; set; }
        public string dateAdd { get; set; }
        public string? dateUpdate { get; set; }
        public List<FounderQuery> Queryfounders { get; set; }
    }
}
