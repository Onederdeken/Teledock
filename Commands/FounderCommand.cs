using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teledock.Models;

namespace Teledock.Commands
{
    public class FounderCommand
    {
        public String Inn{get;set;}
       
        public String FIO{get;set;}

        public static Founder MapToFounder(FounderCommand founder)
        {
            return new Founder
            {
                Inn = founder.Inn,
                FIO = founder.FIO,
                dateAdd = DateOnly.FromDateTime(DateTime.Now),
                dateUpdate = DateOnly.FromDateTime(DateTime.Now),
            };
        }
        public static List<Founder> MapToListFounder(List<FounderCommand> founders)
        {
            var founderList = new List<Founder>();
            founders.ForEach(founder => { 
                founderList.Add(MapToFounder(founder));
            });
            return founderList;
        }
    }
}