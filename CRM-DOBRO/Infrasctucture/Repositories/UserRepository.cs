namespace Infrasctucture.Repositories;

public class UserRepository(CRMDBContext dbContext)
    : BaseRepository<User>(dbContext), IUserRepository
{
    public Task<User?> LogInAsync(string email, string password)
    {
        return DBcontext.Users.FirstOrDefaultAsync(
            u => u.Email == email && u.Password == password);
    }
}
