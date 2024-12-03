using MediatR;
using Teledock.Domain.Enums;

namespace Teledock.Application.Commands
{
    public class FounderCommand : IRequest<(string Message, int code)>
    {
        public int? Id { get; set; }

        public string Inn { get; set; }

        public string FIO { get; set; }
        public Command Command { get; set; }
        public int? ClientId { get; set; }

    }
}