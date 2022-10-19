using IQueueData.Entities;

namespace IQueueData.Interfaces
{
    public interface IQueueRepository : IRepository<Queue>
    {
        Task<IEnumerable<Queue>> GetAllWithDetailsAsync();

        Task<Queue> GetByIdWithDetailsAsync(Guid id);
    }
}
