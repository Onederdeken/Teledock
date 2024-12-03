using System.ComponentModel.DataAnnotations;
using Teledock.Domain.Enums;
using Teledock.Domain.Models;

namespace Teledock.Responses
{
    public class ClientResponse
    {
        public int Id { get; set; }
       
        public string Inn { get; set; }
        public string Name { get; set; }
        public TypeClient Type { get; set; }
        public String dateAdd { get; set; }
        public String? dateUpdate { get; set; }
        public List<FounderResponse> Founders { get; set; }
    }
}
