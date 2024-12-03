using Teledock.Commands;
using Teledock.Domain.Models;
using Teledock.Queries.Clients;
using Teledock.Queries.Founders;
using Teledock.Responses;

namespace Teledock.Abstractions
{
    public interface ICustomMapper
    {
        Client MapToClient(ClientCommand client);
        ClientResponse MapToClientResponse(Client client);
        Founder MapToFounder(FounderCommand founder);
        FounderResponse MapToFounderResponse(Founder founder);
        List<ClientResponse> MapToListClientResponse(List<Client> clients);
        List<Founder> MapToListFounder(List<FounderCommand> founders);
        List<FounderResponse> MapToListFounderResponse(List<Founder> founders);
    }
}