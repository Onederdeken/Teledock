using Teledock.Abstractions;
using Teledock.Commands;
using Teledock.Mapper;
using Teledock.Queries.Founders;
using Teledock.Responses;
using Teledock.Domain.Enums;
using Teledock.Domain.Models;
using System.Collections.Generic;
namespace Teledock.Services
{
    public class FounderService : IFounderService
    {
        private readonly IFounderRepository _FounderRep;
        public FounderService(IFounderRepository founderRepository)
        {
            this._FounderRep = founderRepository;
        }

        public async Task<(string Message, int code)> AddFounder(Founder founder)
        {
            String message = String.Empty;
            int code = 0;
            try
            {
                var client = await _FounderRep.ExistClient(founder.ClientId);
                if (client == null) return ("такого клиента не существует", 400);
                else if (client._TypeClient == TypeClient.IP) return ("Вы пытаетесь добавить учредителя к ИП", 400);
                await _FounderRep.AddFounder(founder);
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
            String Message = String.Empty;
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

        public async Task<(string Error, List<Founder> Founders)> getAllFounders()
        {
            String Error = String.Empty;
            List<Founder> founders = null;
            try
            {
               founders = await _FounderRep.getAllFounders();
            }
            catch (System.Exception ex)
            {
                Error = ex.Message;
            }
            return (Error, founders);
        }

        public async Task<(string Error, Founder Founder)> getFounderById(int FounderId)
        {
            String Error = String.Empty;
            Founder founder = null;
            try
            {
                if (!await _FounderRep.ExistFounder(FounderId)) return ("такого учредителя не существует", null);
                founder = await _FounderRep.getFounderById(FounderId);
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return (Error, founder);
        }

        public async Task<(string Message, int code)> UpdateFounder(Founder founder)
        {
            String message = String.Empty;
            int code = 0;
            try
            {
                if (!await _FounderRep.ExistFounder(founder.Id)) return ("такого учредителя не существует", 400);;
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
