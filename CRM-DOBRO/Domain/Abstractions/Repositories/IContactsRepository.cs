using Domain.Entities;

namespace Domain.Abstractions.Repositories
{
    public interface IContactRepository : IRepository<Contact>
    {
        Task<List<Contact>> FoundContactLeadsAsync();
    }
}
