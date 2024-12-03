using MediatR;
using Teledock.Domain.Enums;
namespace Teledock.Commands
{

    public class ClientCommand : IRequest<(string Message, int code)>
    {
        public int Id { get; set; }
       
        public string Inn { get; set; }

        public string Name { get; set; }
        public Command comand { get; set; }
        public TypeClient _TypeClient { get; set; }
    }
}
