
using Teledock.Domain.Models;
using Teledock.Domain.Responses;

namespace Teledock.Domain.Abstractions
{
    public interface IFounderService
    {
        public Task<(string Error, List<FounderResponse> Founders)> getAllFounders();
        public Task<(string Error, FounderResponse Founder)> getFounderById(int FounderId);
        public Task<(string Message, int code)> AddFounder(Founder founder, int ClientID);
        public Task<(string Message, int code)> UpdateFounder(Founder founder);
        public Task<(string Message, int code)> DeleteFounder(int FounderId);
        public Task<(string Message, int code)> ChangeClient(int FounderId, int ClientId);

    }
}
