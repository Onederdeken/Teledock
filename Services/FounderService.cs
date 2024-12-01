using Microsoft.AspNetCore.JsonPatch.Operations;
using Teledock.Abstractions;
using Teledock.Commands;
using Teledock.Mapper;
using Teledock.Models;
using Teledock.Queries.Founders;
using Teledock.Repositories;
namespace Teledock.Services
{
    public class FounderService : IFounderService
    {
        private readonly IFounderRepository _FounderRep;
        public FounderService(IFounderRepository founderRepository)
        {
            this._FounderRep = founderRepository;
        }

        public async Task<(string Message, int code)> AddFounder(FounderCommand founder, int ClientID)
        {
            String message = String.Empty;
            int code = 0;
            try
            {
                if (await _FounderRep.ExistClient(ClientID) == false) return ("такого клиента не существует", 400);
                using (var mapper = new CustomMapper())
                {
                    await _FounderRep.AddFounder(ClientID, mapper.MapToFounder(founder));
                    message = "Добавление прошло успешно";
                    code = 200;
                }

            }
            catch (Exception ex)
            {
                message = ex.Message;
                code = 400;
            }
            return (message, code);
        }

        public async Task<(string Message, int code)> ChangeClient(int FounderId, int ClientId)
        {
            String Message = String.Empty;
            int code = 0;
            try
            {
                if (await _FounderRep.ExistClient(ClientId) && await _FounderRep.ExistFounder(FounderId))
                {
                    await _FounderRep.ChangeClient(FounderId, ClientId);
                    Message = "смена клиента прошла успешно";
                    code = 200;
                }
                else if (await _FounderRep.ExistFounder(FounderId) == false)
                {
                    Message = "Такого учредителя не существует";
                    code = 400;
                }
                else
                {
                    Message = "Такого клиента не существует";
                    code = 400;
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                code = 400;
            }
            return (Message, code);
        }

        public async Task<(string Message, int code)> DeleteFounder(int FounderId)
        {
            String Message = String.Empty;
            int code = 0;
           
            try
            {
                if (await _FounderRep.ExistFounder(FounderId))
                {
                    await _FounderRep.DeleteFounder(FounderId);
                    Message = "удаление прошло успешно";
                    code = 200;
                }
                else
                {
                    Message = "такого учредителя не существует";
                    code = 400;
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                code = 400;
            }
            return (Message, code);
        }

        public async Task<(string Error, List<FounderQuery> Founders)> getAllFounders()
        {
            String Error = String.Empty;
            List<FounderQuery> founderQueries = null;
            try
            {
                var founders = await _FounderRep.getAllFounders();
                using (var mapper = new CustomMapper())
                {
                    founderQueries = mapper.MapToListFounderQuery(founders);
                }

            }
            catch (System.Exception ex)
            {
                Error = ex.Message;
            }
            return (Error, founderQueries);
        }

        public async Task<(string Error, FounderQuery Founder)> getFounderById(int FounderId)
        {
            String Error = String.Empty;
            FounderQuery FounderQuery = new FounderQuery();
            try
            {
                if (!await _FounderRep.ExistFounder(FounderId)) return ("такого учредителя не существует", null);
                var founder = await _FounderRep.getFounderById(FounderId);
                using (var mapper = new CustomMapper())
                {
                    FounderQuery = mapper.MapToFounderQuery(founder);
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return (Error, FounderQuery);
        }

        public async Task<(string Message, int code)> UpdateFounder(FounderCommand founderCommand, int founderID)
        {
            String message = String.Empty;
            int code = 0;
            try
            {
                if (!await _FounderRep.ExistFounder(founderID)) return ("такого учредителя не существует", 400);
                using (var mapper = new CustomMapper())
                {
                    var founder = mapper.MapToFounder(founderCommand);
                    founder.Id = founderID;
                    await _FounderRep.UpdateFounder(founder);
                }
                message = "обновление прошло удачно";
                code = 200;

            }
            catch (Exception ex)
            {
                message = ex.Message;
                code = 400;
            }
            return (message, code);
        }
    }
}
