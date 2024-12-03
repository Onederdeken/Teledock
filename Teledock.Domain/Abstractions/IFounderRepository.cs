using Teledock.Domain.Models;

namespace Teledock.Abstractions
{
    public interface IFounderRepository
    {
        public Task<List<Founder>> getAllFounders();
        public Task<Founder> getFounderById(int FounderId);
        public Task UpdateFounder(Founder founder);
        public Task DeleteFounder(int FounderId);
        public Task AddFounder(Founder founder);
        public Task<bool> ExistFounder(int FounderId);
        public Task ChangeClient(int FounderId, int ClientId);
        public Task<Client?> ExistClient(int Id);

    }
}
