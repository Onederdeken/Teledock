using Teledock.Commands;
using Teledock.Models;
using Teledock.Queries.Clients;
using Teledock.Queries.Founders;

namespace Teledock.Abstractions
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