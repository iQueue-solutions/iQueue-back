using IQueueData.Entities;

namespace IQueueData.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity?> GetByIdAsync(Guid id);

        Task<Guid> AddAsync(TEntity entity);

        void Delete(TEntity entity);

        Task DeleteByIdAsync(Guid id);

        void Update(TEntity entity);
    }
}
