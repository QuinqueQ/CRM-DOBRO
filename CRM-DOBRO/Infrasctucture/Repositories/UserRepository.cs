namespace Infrasctucture.Repositories
{
    public class UserRepository(CRMDBContext dbContext)
#pragma warning disable CS9107 
        : BaseRepository<User>(dbContext), IUserRepository
#pragma warning restore CS9107 
    {
        public Task<User?> LogInAsync(string email, string password)
        {
            return  dbContext.Users.FirstOrDefaultAsync(
                u => u.Email == email && u.Password == password);
        }
    }
}
