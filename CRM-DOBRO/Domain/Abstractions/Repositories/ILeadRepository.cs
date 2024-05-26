using Domain.Entities;

namespace Domain.Abstractions.Repositories;

public interface ILeadRepository : IBaseRepository<Lead>
{
    Task<List<Lead>> GetLeadsBySalerIdAsync(int salerId);
    Task<Contact?> FoundContactLeadAsync(int contactId);

}
