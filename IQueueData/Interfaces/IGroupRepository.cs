using IQueueData.Entities;

namespace IQueueData.Interfaces
{
    public interface IGroupRepository : IRepository<Group>
    {
        Task<IEnumerable<Group>> GetAllWithDetailsAsync();

        Task<Group> GetByIdWithDetailsAsync(Guid id);
    }
}
