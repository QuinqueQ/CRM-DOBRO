namespace Domain.Abstractions
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(int id);
    }
}
