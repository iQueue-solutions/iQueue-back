using IQueueData.Entities;

namespace IQueueData.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetAllWithDetailsAsync();

        Task<User> GetByIdWithDetailsAsync(Guid id);
    }
}
