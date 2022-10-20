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
    public class QueueRepository : IQueueRepository
    {
        private readonly QueueDbContext _queueDbContext;

        public QueueRepository(QueueDbContext queueDbContext)
        {
            _queueDbContext = queueDbContext;
        }

        public async Task AddAsync(Queue entity)
        {
            await _queueDbContext.Queues.AddAsync(entity);
        }

        public void Delete(Queue entity)
        {
            _queueDbContext.Queues.Remove(entity);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var entity = await _queueDbContext.Queues.FirstOrDefaultAsync(x => x.Id.Equals(id));
            _queueDbContext.Queues.Remove(entity);
            await _queueDbContext.SaveChangesAsync();
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
                .Include(x => x.QueueRecords)
                );
        }

        public async Task<Queue> GetByIdAsync(Guid id)
        {
            return await _queueDbContext.Queues.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<Queue> GetByIdWithDetailsAsync(Guid id)
        {
            return await (
                _queueDbContext.Queues                
                .Include(x => x.QueueRecords).
                FirstOrDefaultAsync(x => x.Id.Equals(id))
                );
        }

        public void Update(Queue entity)
        {
            _queueDbContext.Queues.Update(entity);
            _queueDbContext.SaveChanges();
        }
    }
}
