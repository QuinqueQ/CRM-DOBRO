using Domain.Entities;

namespace Domain.Abstractions.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> LogInAsync(string email, string password);
}
