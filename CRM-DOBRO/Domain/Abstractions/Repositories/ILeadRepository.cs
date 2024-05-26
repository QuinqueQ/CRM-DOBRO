using Domain.Entities;

namespace Domain.Abstractions.Repositories
{
    public interface ILeadRepository : IRepository<Lead>
    {
        Task<List<Lead>> GetLeadsBySalerIdAsync(int salerId);
        Task<Contact?> FoundContactLeadAsync(int contactId);

    }
}
