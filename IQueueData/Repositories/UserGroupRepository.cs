using IQueueData.Entities;
using IQueueData.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IQueueData.Repositories
{
    public class UserGroupRepository : IUserGroupRepository
    {
        private readonly QueueDbContext _queueDbContext;

        public UserGroupRepository(QueueDbContext queueDbContext)
        {
            _queueDbContext = queueDbContext;
        }

        public async Task<Guid> AddAsync(UserGroup entity)
        {
            await _queueDbContext.UserGroups.AddAsync(entity);
            return entity.Id;
        }

        public void Delete(UserGroup entity)
        {
            _queueDbContext.UserGroups.Remove(entity);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var entity = await _queueDbContext.UserGroups.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (entity != null) _queueDbContext.UserGroups.Remove(entity);
        }

        public async Task<IEnumerable<UserGroup>> GetAllAsync()
        {
            return await _queueDbContext.UserGroups.ToListAsync();
        }
        
        public async Task<UserGroup> GetByIdAsync(Guid id)
        {
            return await _queueDbContext.UserGroups.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }
        
        public void Update(UserGroup entity)
        {
            _queueDbContext.UserGroups.Update(entity);
        }
    }
}
