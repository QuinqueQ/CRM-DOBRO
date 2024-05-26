using Domain.Entities;

namespace Domain.Abstractions.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> LogInAsync(string email, string password);
    }
}
