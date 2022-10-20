using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IQueueData.Entities;
using IQueueData.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IQueueData.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly QueueDbContext _queueDbContext;

        public GroupRepository(QueueDbContext queueDbContext)
        {
            _queueDbContext = queueDbContext;
        }

        public async Task AddAsync(Group entity)
        {
            await _queueDbContext.Groups.AddAsync(entity);
        }

        public void Delete(Group entity)
        {
            _queueDbContext.Groups.Remove(entity);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var entity = await _queueDbContext.Groups.FirstOrDefaultAsync(x => x.Id.Equals(id));
            _queueDbContext.Groups.Remove(entity);
            await _queueDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Group>> GetAllAsync()
        {
            var result = await _queueDbContext.Groups.ToListAsync();
            return result;
        }

        public Task<IEnumerable<Group>> GetAllWithDetailsAsync()
        {
            return Task.FromResult<IEnumerable<Group>>(
                _queueDbContext.Groups
                .Include(x => x.UserGroups)                
                );
        }

        public async Task<Group> GetByIdAsync(Guid id)
        {
            return await _queueDbContext.Groups.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<Group> GetByIdWithDetailsAsync(Guid id)
        {
            return await (
                _queueDbContext.Groups
                .Include(x => x.UserGroups)                
                .FirstOrDefaultAsync(x => x.Id.Equals(id))
                );
        }

        public void Update(Group entity)
        {
            _queueDbContext.Groups.Update(entity);
            _queueDbContext.SaveChanges();
        }
    }
}
