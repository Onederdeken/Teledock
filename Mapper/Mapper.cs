using Teledock.Commands;
using Teledock.Models;
using Teledock.Queries;

namespace Teledock.Mapper
{
    public class CustomMapper : IDisposable
    {
        public void Dispose()
        {

        }

        public Founder MapToFounder(FounderCommand founder)
        {
            return new Founder
            {
                Inn = founder.Inn,
                FIO = founder.FIO,
                dateAdd = DateOnly.FromDateTime(DateTime.Now),
                dateUpdate = DateOnly.FromDateTime(DateTime.Now),
            };
        }
        public  List<Founder> MapToListFounder(List<FounderCommand> founders)
        {
            var founderList = new List<Founder>();
            founders.ForEach(founder => {
                founderList.Add(MapToFounder(founder));
            });
            return founderList;
        }
        public Client MapToClient(ClientIPCommand client)
        {
            return new Client
            {
                Inn = client.Inn,
                Name = client.Name,
                _TypeClient = TypeClient.IP,
                dateAdd = DateOnly.FromDateTime(DateTime.Now),
                dateUpdate = DateOnly.FromDateTime(DateTime.Now),
            };
        }
        public Client MapToClient(ClientULCommand client)
        {
            return new Client
            {
                Inn = client.Inn,
                Name = client.Name,
                _TypeClient = TypeClient.UL,
                dateAdd = DateOnly.FromDateTime(DateTime.Now),
                dateUpdate = DateOnly.FromDateTime(DateTime.Now),

            };
        }
        public  ClientQuery MapToClientQuery(Client client)
        {
            return new ClientQuery
            {
                Id = client.Id,
                Inn = client.Inn,
                Name = client.Name,
                Type = client._TypeClient,
                dateAdd = client.dateAdd,
                dateUpdate = client.dateUpdate,
                Queryfounders = MapToListFounderQuery(client.founders)
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
        public  List<FounderQuery> MapToListFounderQuery(List<Founder> founders)
        {
            var QueryFounders = new List<FounderQuery>();
            founders.ForEach(founder =>
            {
                QueryFounders.Add(MapToFounderQuery(founder));
            });
            return QueryFounders;
        }
        public  FounderQuery MapToFounderQuery(Founder founder)
        {
            return new FounderQuery
            {
                Id = founder.Id,
                Inn = founder.Inn,
                FIO = founder.FIO,
                dateAdd = founder.dateAdd,
                dateUpdate = founder.dateUpdate
            };
        }
    }
}
