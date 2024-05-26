namespace Infrasctucture.Repositories.Abstractions;

public class BaseRepository<TEntity>(CRMDBContext dbContext) : IBaseRepository<TEntity> where TEntity : Entity
{
    protected CRMDBContext DBcontext => dbContext;
    public async Task AddAsync(TEntity entity)
    {
        await dbContext.AddAsync(entity);
    }

    public void Remove(TEntity entity)
    {
        dbContext.Set<TEntity>().Remove(entity);
    }

    public void Update(TEntity entity)
    {
        dbContext.Set<TEntity>().Update(entity);
    }

    public virtual Task<List<TEntity>> GetAllAsync()
    {
        return dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await dbContext.Set<TEntity>().AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);
    }



}
