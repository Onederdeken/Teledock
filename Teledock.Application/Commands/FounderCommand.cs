using MediatR;
using Teledock.Domain.Enums;


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