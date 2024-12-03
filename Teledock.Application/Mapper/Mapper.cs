using Teledock.Abstractions;
using Teledock.Commands;
using Teledock.Domain.Enums;
using Teledock.Domain.Models;
using Teledock.Queries.Clients;
using Teledock.Queries.Founders;
using Teledock.Responses;

namespace Teledock.Mapper
{
    public class CustomMapper : IDisposable, ICustomMapper
    {
        public void Dispose()
        {

        }
        public Founder MapToFounder(FounderCommand founder)
        {

            return new Founder
            {
                Id = (int)founder.Id,
                Inn = founder.Inn,
                FIO = founder.FIO,
                ClientId = (int)founder.ClientId
            };
        }
        public List<Founder> MapToListFounder(List<FounderCommand> founders)
        {
            var founderList = new List<Founder>();
            founders.ForEach(founder =>
            {
                founderList.Add(MapToFounder(founder));
            });
            return founderList;
        }
        public Client MapToClient(ClientCommand client)
        {

            return new Client
            {
                Id = (int)client.Id,
                Inn = client.Inn,
                Name = client.Name,
                _TypeClient = client._TypeClient
            };
        }
        public ClientResponse MapToClientResponse(Client client)
        {
            return new ClientResponse
            {
                Id = client.Id,
                Inn = client.Inn,
                Name = client.Name,
                Type = client._TypeClient,
                dateAdd = client.dateAdd.ToString("dd.MM.yyyy HH:mm"),
                dateUpdate = client.dateUpdate.HasValue ? client.dateUpdate.Value.ToString("dd.MM.yyyy HH:mm") : "еще не было обновлений",
                Founders = client._TypeClient == TypeClient.IP ? null : client.founders == null ? new List<FounderResponse>() : MapToListFounderResponse(client.founders)
            };
        }
        public List<ClientResponse> MapToListClientResponse(List<Client> clients)
        {
            var listclients = new List<ClientResponse>();
            clients.ForEach(client =>
            {
                listclients.Add(MapToClientResponse(client));
            });
            return listclients;
        }
        public List<FounderResponse> MapToListFounderResponse(List<Founder> founders)
        {
            var QueryFounders = new List<FounderResponse>();
            founders.ForEach(founder =>
            {
                QueryFounders.Add(MapToFounderResponse(founder));
            });
            return QueryFounders;
        }
        public FounderResponse MapToFounderResponse(Founder founder)
        {
            return new FounderResponse
            {
                Id = founder.Id,
                Inn = founder.Inn,
                FIO = founder.FIO,
                dateAdd = founder.dateAdd.ToString("dd.MM.yyyy HH:mm"),
                dateUpdate = founder.dateUpdate.HasValue ? founder.dateUpdate.Value.ToString("dd.MM.yyyy HH:mm") : "еще не было обновлений"
            };
        }
    }
}
