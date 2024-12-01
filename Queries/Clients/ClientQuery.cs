using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Teledock.Models;
using Teledock.Queries.Founders;
using Teledock.Responses;

namespace Teledock.Queries.Clients
{
    public class ClientQuery : IRequest<(string Error, ClientResponse ClientQuery)>
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