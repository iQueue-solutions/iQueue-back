using IQueueData.Entities;
using IQueueData.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IQueueData.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly QueueDbContext _queueDbContext;

        public UserRepository(QueueDbContext queueDbContext)
        {
            _queueDbContext = queueDbContext;
        }

        public async Task AddAsync(User entity)
        {
            await _queueDbContext.Users.AddAsync(entity);
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            // without Include (read about Include)
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllWithDetailsAsync()
        {
            return Task.FromResult<IEnumerable<User>>(
                _queueDbContext.Users
                .Include(x => x.UserGroups)
                .Include(x => x.QueueRecords)
                );
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _queueDbContext.Users.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public Task<User> GetByIdWithDetailsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
