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

        public async Task<Guid> AddAsync(User entity)
        {
            await _queueDbContext.Users.AddAsync(entity);
            return entity.Id;
        }

        public async void Delete(User entity)
        {
            await _queueDbContext.Queues.Where(x => x.AdminId == entity.Id).LoadAsync();
            
            _queueDbContext.Users.Remove(entity);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            await _queueDbContext.Queues.Where(x => x.AdminId == id).LoadAsync();
            
            var entity = await _queueDbContext.Users.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (entity != null) _queueDbContext.Users.Remove(entity);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _queueDbContext.Users.ToListAsync();
        }

        public Task<IEnumerable<User>> GetAllWithDetailsAsync()
        {
            return Task.FromResult<IEnumerable<User>>(
                _queueDbContext.Users
                .Include(x => x.UserGroups)
                .Include(x => x.UserInQueues));
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _queueDbContext.Users.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<User> GetByIdWithDetailsAsync(Guid id)
        {
            return await 
                _queueDbContext.Users
                .Include(x => x.UserGroups)
                .Include(x => x.UserInQueues).
                FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public void Update(User entity)
        {
            _queueDbContext.Users.Update(entity);
        }
    }
}
