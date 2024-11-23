using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teledock.Queries
{
    public class ClientQuery
    {
        public int Id{get;set;}
        public String Inn{get;set;}
        public String Name{get;set;}
        public String Type{get;set;}
        public DateOnly dateAdd{get;set;}
        public DateOnly dateUpdate{get;set;}
        public List<FounderQuery> Queryfounders{get;set;}
    }
}