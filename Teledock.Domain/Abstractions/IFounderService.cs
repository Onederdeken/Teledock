using Teledock.Domain.Models;
using Teledock.Responses;

namespace Teledock.Abstractions
{
    public interface IFounderService
    {
        public Task<(String Error, List<Founder> Founders)> getAllFounders();
        public Task<(String Error, Founder Founder)> getFounderById(int FounderId);
        public Task<(String Message, int code)> AddFounder(Founder founder);
        public Task<(String Message, int code)> UpdateFounder(Founder founder);
        public Task<(String Message, int code)> DeleteFounder(int FounderId);
        public Task<(String Message, int code)> ChangeClient(int FounderId, int ClientId);
       
    }
}
