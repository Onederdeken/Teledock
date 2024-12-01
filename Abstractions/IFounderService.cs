using Teledock.Commands;
using Teledock.Models;
using Teledock.Queries.Founders;
using Teledock.Responses;

namespace Teledock.Abstractions
{
    public interface IFounderService
    {
        public Task<(String Error,List<FounderResponse> Founders)> getAllFounders();
        public Task<(String Error, FounderResponse Founder)> getFounderById(int FounderId);
        public Task<(String Message, int code)> AddFounder(FounderCommand founder, int ClientID);
        public Task<(String Message, int code)> UpdateFounder(FounderCommand founder, int FounderID);
        public Task<(String Message, int code)> DeleteFounder(int FounderId);
        public Task<(String Message, int code)> ChangeClient(int FounderId, int ClientId);
       
    }
}
