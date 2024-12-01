using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teledock.Responses;

namespace Teledock.Queries.Founders
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