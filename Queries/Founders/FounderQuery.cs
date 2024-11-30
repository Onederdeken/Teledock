using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teledock.Queries
{
    public class FounderQuery : IRequest<(String Error, FounderQuery FounderQuery)>
    {
        public int Id{get;set;}
        
        public String Inn{get;set;}
        
        public String FIO{get;set;}
        
        public String dateAdd{get;set;}
        
        public String? dateUpdate{get;set;}
    }
}