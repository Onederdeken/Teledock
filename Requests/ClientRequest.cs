using Teledock.Models;
using Teledock.Queries;

namespace Teledock.Requests
{
    public class ClientRequest
    {
        
        public String Inn { get; set; }
        public String Name { get; set; }
        public TypeClient Type { get; set; }
        public String dateAdd { get; set; }
        public String? dateUpdate { get; set; }
        public List<FounderQuery> Queryfounders { get; set; }
    }
}
