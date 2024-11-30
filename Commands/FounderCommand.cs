using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Teledock.Models;

namespace Teledock.Commands
{
    public class FounderCommand:IRequest<(String Message, int code)>
    {
        public int? Id { get; set; }
       
        public String Inn{get;set;}
       
        public String FIO{get;set;}
        public Command Command { get; set; }
        public int? ClientId { get;set; }
  
    }
}