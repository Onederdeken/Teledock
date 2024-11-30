using MediatR;
using Teledock.Models;

namespace Teledock.Queries
{
    public class ClientsQueries : IRequest<(String Error, List<ClientQuery> ClientQueries)>
    {
        public int Id { get; set; }
        public String Inn { get; set; }
        public String Name { get; set; }
        public TypeClient Type { get; set; }
        public String dateAdd { get; set; }
        public String? dateUpdate { get; set; }
        public List<FounderQuery> Queryfounders { get; set; }
    }
}
