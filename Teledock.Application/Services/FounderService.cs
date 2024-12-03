
using Teledock.Domain.Abstractions;
using Teledock.Application.Commands;
using Teledock.Application.Queries.Founders;
using Teledock.Application.Mapper;
using Teledock.Domain.Responses;
using Teledock.Domain.Enums;
using Teledock.Domain.Models;
namespace Teledock.Application.Services
{
    public class FounderService : IFounderService
    {
        private readonly IFounderRepository _FounderRep;
        public FounderService(IFounderRepository founderRepository)
        {
            _FounderRep = founderRepository;
        }

        public async Task<(string Message, int code)> AddFounder(Founder founder, int ClientID)
        {
            string message = string.Empty;
            int code = 0;
            try
            {
                var client = await _FounderRep.ExistClient(ClientID);
                if (client == null) return ("такого клиента не существует", 400);
                else if (client._TypeClient == TypeClient.IP) return ("Вы пытаетесь добавить учредителя к ИП", 400);
                await _FounderRep.AddFounder(ClientID,founder);
                message = "Добавление прошло успешно";
                code = 200;

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
            string Message = string.Empty;
            int code = 0;
            try
            {
                if (await _FounderRep.ExistClient(ClientId) != null && await _FounderRep.ExistFounder(FounderId))
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
            string Message = string.Empty;
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

        public async Task<(string Error, List<FounderResponse> Founders)> getAllFounders()
        {
            string Error = string.Empty;
            List<FounderQuery> founderQueries = null;
            List<FounderResponse> founderResponses = new List<FounderResponse>();
            try
            {
                var founders = await _FounderRep.getAllFounders();
                using (var mapper = new CustomMapper())
                {
                    founderQueries = mapper.MapToListFounderQuery(founders);
                    founderResponses = mapper.MapToListFounderResponse(founderQueries);
                }

            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return (Error, founderResponses);
        }

        public async Task<(string Error, FounderResponse Founder)> getFounderById(int FounderId)
        {
            string Error = string.Empty;
            FounderQuery FounderQuery = new FounderQuery();
            FounderResponse founderResponse = new FounderResponse();
            try
            {
                if (!await _FounderRep.ExistFounder(FounderId)) return ("такого учредителя не существует", null);
                var founder = await _FounderRep.getFounderById(FounderId);
                using (var mapper = new CustomMapper())
                {
                    FounderQuery = mapper.MapToFounderQuery(founder);
                    founderResponse = mapper.MapToFounderResponse(FounderQuery);
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return (Error, founderResponse);
        }

        public async Task<(string Message, int code)> UpdateFounder(Founder founder)
        {
            string message = string.Empty;
            int code = 0;
            try
            {
                if (!await _FounderRep.ExistFounder(founder.Id)) return ("такого учредителя не существует", 400);
                await _FounderRep.UpdateFounder(founder);
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
