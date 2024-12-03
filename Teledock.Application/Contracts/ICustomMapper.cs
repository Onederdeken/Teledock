using Teledock.Application.Commands;
using Teledock.Domain.Models;
using Teledock.Application.Queries.Clients;
using Teledock.Application.Queries.Founders;

namespace Teledock.Domain.Abstractions
{
    public interface ICustomMapper
    {
        Client MapToClient(ClientCommand client);
        ClientQuery MapToClientQuery(Client client);
        Founder MapToFounder(FounderCommand founder);
        FounderQuery MapToFounderQuery(Founder founder);
        List<ClientQuery> MapToListClientQuery(List<Client> clients);
        List<Founder> MapToListFounder(List<FounderCommand> founders);
        List<FounderQuery> MapToListFounderQuery(List<Founder> founders);
    }
}