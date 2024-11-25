using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teledock.Queries
{
    public class FounderQuery
    {
        public int Id{get;set;}
        
        public String Inn{get;set;}
        
        public String FIO{get;set;}
        
        public DateOnly dateAdd{get;set;}
        
        public DateOnly dateUpdate{get;set;}
    }
}