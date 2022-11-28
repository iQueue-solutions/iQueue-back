using IQueueData.Entities;
using IQueueData.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IQueueData.Repositories
{
    public class QueueRepository : IQueueRepository
    {
        private readonly QueueDbContext _queueDbContext;

        public QueueRepository(QueueDbContext queueDbContext)
        {
            _queueDbContext = queueDbContext;
        }

        public async Task<Guid> AddAsync(Queue entity)
        {
            await _queueDbContext.Queues.AddAsync(entity);
            return entity.Id;
        }

        public void Delete(Queue entity)
        {
            _queueDbContext.Queues.Remove(entity);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var entity = await _queueDbContext.Queues.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (entity != null) _queueDbContext.Queues.Remove(entity);
        }

        public async Task<IEnumerable<Queue>> GetAllAsync()
        {
            var result = await _queueDbContext.Queues.ToListAsync();
            return result;
        }

        public Task<IEnumerable<Queue>> GetAllWithDetailsAsync()
        {
            return Task.FromResult<IEnumerable<Queue>>(
                _queueDbContext.Queues                
                .Include(x => x.QueueUsers));
        }

        public async Task<Queue> GetByIdAsync(Guid id)
        {
            return await _queueDbContext.Queues.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<Queue> GetByIdWithDetailsAsync(Guid id)
        {
            return await _queueDbContext.Queues                
                .Include(x => x.QueueUsers)
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public void Update(Queue entity)
        {
            _queueDbContext.Queues.Update(entity);
        }
    }
}
