using System.ComponentModel.DataAnnotations;
using Teledock.Models;
using System.ComponentModel.DataAnnotations.Schema;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using MediatR;
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

    public enum Command
    {
        Add,
        Update,
        Delete,
        ChangeClient
    }

}
