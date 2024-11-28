using Teledock.Abstractions;
using Teledock.Commands;
using Teledock.Models;
using Teledock.Queries;

namespace Teledock.Mapper
{
    public class CustomMapper : IDisposable, ICustomMapper
    {
        public void Dispose()
        {

        }

        public Founder MapToFounder(FounderCommand founder, operation operation)
        {
            Founder Founder = operation switch
            {
                operation.Add => new Founder {
                    Inn = founder.Inn,
                    FIO = founder.FIO,
                    dateAdd = DateOnly.FromDateTime(DateTime.Now),
                    dateUpdate = DateOnly.FromDateTime(DateTime.Now),
                },
                operation.Update => new Founder {
                    Inn = founder.Inn,
                    FIO = founder.FIO,
                    dateUpdate = DateOnly.FromDateTime(DateTime.Now),
                }
            };
            return Founder;
        }
        public List<Founder> MapToListFounder(List<FounderCommand> founders, operation operation)
        {
            var founderList = new List<Founder>();
            founders.ForEach(founder =>
            {
                founderList.Add(MapToFounder(founder, operation));
            });
            return founderList;
        }
        public Client MapToClient(ClientIPCommand client, operation operation)
        {
            Client Client = operation switch
            {
                operation.Add => new Client
                {
                    Inn = client.Inn,
                    Name = client.Name,
                    _TypeClient = TypeClient.IP,
                    dateAdd = DateOnly.FromDateTime(DateTime.Now),
                    dateUpdate = DateOnly.FromDateTime(DateTime.Now),
                },
                operation.Update => new Client
                {
                    Inn = client.Inn,
                    Name = client.Name,
                    _TypeClient = TypeClient.IP,
                    dateUpdate = DateOnly.FromDateTime(DateTime.Now),
                }
            };
            return Client;
        }
        public Client MapToClient(ClientULCommand client, operation operation)
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
        public ClientQuery MapToClientQuery(Client client)
        {
            return new ClientQuery
            {
                Id = client.Id,
                Inn = client.Inn,
                Name = client.Name,
                Type = client._TypeClient,
                dateAdd = client.dateAdd,
                dateUpdate = client.dateUpdate,
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
                dateAdd = founder.dateAdd,
                dateUpdate = founder.dateUpdate
            };
        }
    }
}
