namespace Infrasctucture.Data
{
    public class UnitOfWork(CRMDBContext dbContext) : IUnitOfWork
    {
        public async Task SaveChangesAsync()
        { 
            await dbContext.SaveChangesAsync();
        }
    }
}
