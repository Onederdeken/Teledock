using Teledock.Commands;

using Teledock.Models;
using Teledock.Queries;

namespace Teledock.Abstractions
{
    public interface ICustomMapper
    {
        Client MapToClient(ClientIPCommand client, operation operation);
        Client MapToClient(ClientULCommand client, operation operation);
        ClientQuery MapToClientQuery(Client client);
        Founder MapToFounder(FounderCommand founder, operation operation);
        FounderQuery MapToFounderQuery(Founder founder);
        List<ClientQuery> MapToListClientQuery(List<Client> clients);
        List<Founder> MapToListFounder(List<FounderCommand> founders, operation operation);
        List<FounderQuery> MapToListFounderQuery(List<Founder> founders);
    }
    public enum operation
    {
        Add,
        Update
    }
}