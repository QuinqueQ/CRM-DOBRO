using Domain.Entities;

namespace Domain.Abstractions.Repositories;

public interface IContactRepository : IBaseRepository<Contact>
{
    Task<List<Contact>> GetContactLeadsAsync();
}
