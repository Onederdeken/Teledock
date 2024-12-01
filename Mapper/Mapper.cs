using Teledock.Abstractions;
using Teledock.Commands;
using Teledock.Models;
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
        public List<ClientResponse> MapToListClientResponse(List<ClientQuery> clientQueries)
        {
            List<ClientResponse> clientResponses = new List<ClientResponse>();
            clientQueries.ForEach(client =>
            {
                clientResponses.Add(MapToClientResponse(client));
            });
            return clientResponses;
        }
        public ClientResponse MapToClientResponse(ClientQuery clientQuery)
        {
            return new ClientResponse
            {
                Id = clientQuery.Id,
                Inn = clientQuery.Inn,
                Name = clientQuery.Name,
                Type = clientQuery.Type,
                dateAdd = clientQuery.dateAdd,
                dateUpdate = clientQuery.dateUpdate,
                Founders = clientQuery.Type == TypeClient.IP ? null : clientQuery.Queryfounders == null ? new List<FounderResponse>() : MapToListFounderResponse(clientQuery.Queryfounders)
            };
        }
        public List<FounderResponse> MapToListFounderResponse(List<FounderQuery> founderQueries)
        {
            List<FounderResponse> founderResponses = new List<FounderResponse>();
            founderQueries.ForEach(query =>
            {
                founderResponses.Add(MapToFounderResponse(query));
            });
            return founderResponses;
        }
        public FounderResponse MapToFounderResponse(FounderQuery founderQuery)
        {
            return new FounderResponse()
            {
                Id = founderQuery.Id,
                Inn = founderQuery.Inn,
                FIO = founderQuery.FIO,
                dateAdd = founderQuery.dateAdd,
                dateUpdate = founderQuery.dateUpdate,
            };
        }

        public Founder MapToFounder(FounderCommand founder)
        {

            return new Founder
            {
                Inn = founder.Inn,
                FIO = founder.FIO,
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
                Inn = client.Inn,
                Name = client.Name,
                _TypeClient = client._TypeClient

            };
        }
        public ClientQuery MapToClientQuery(Client client)
        {
            return new ClientQuery
            {
                Id = client.Id,
                Inn = client.Inn,
                Name = client.Name,
                Type = client._TypeClient,
                dateAdd = client.dateAdd.ToString("dd.MM.yyyy HH:mm"),
                dateUpdate = client.dateUpdate.HasValue ? client.dateUpdate.Value.ToString("dd.MM.yyyy HH:mm") : "еще не было обновлений",
                Queryfounders = client._TypeClient == TypeClient.IP ? null : client.founders == null ? new List<FounderQuery>() : MapToListFounderQuery(client.founders)
            };
        }
        public List<ClientQuery> MapToListClientQuery(List<Client> clients)
        {
            var listclients = new List<ClientQuery>();
            clients.ForEach(client =>
            {
                listclients.Add(MapToClientQuery(client));
            });
            return listclients;
        }
        public List<FounderQuery> MapToListFounderQuery(List<Founder> founders)
        {
            var QueryFounders = new List<FounderQuery>();
            founders.ForEach(founder =>
            {
                QueryFounders.Add(MapToFounderQuery(founder));
            });
            return QueryFounders;
        }
        public FounderQuery MapToFounderQuery(Founder founder)
        {
            return new FounderQuery
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
