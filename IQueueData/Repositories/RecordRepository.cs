using IQueueData.Entities;
using IQueueData.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IQueueData.Repositories
{
    public class RecordRepository : IRecordRepository
    {
        private readonly QueueDbContext _queueDbContext;

        public RecordRepository(QueueDbContext queueDbContext)
        {
            _queueDbContext = queueDbContext;
        }

        public async Task AddAsync(QueueRecord entity)
        {
            await _queueDbContext.QueueRecords.AddAsync(entity);
        }

        public void Delete(QueueRecord entity)
        {
            _queueDbContext.QueueRecords.Remove(entity);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var entity = await _queueDbContext.QueueRecords
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
            
           await _queueDbContext.Users.Where(x => x.Id == entity!.UserId).LoadAsync();;
           await _queueDbContext.Queues.Where(x => x.Id == entity!.QueueId).LoadAsync();;
            
           if (entity != null) _queueDbContext.QueueRecords.Remove(entity);
        }

        public async Task<IEnumerable<QueueRecord>> GetAllAsync()
        {
            var result = await _queueDbContext.QueueRecords.ToListAsync();
            return result;
        }
                

        public async Task<QueueRecord> GetByIdAsync(Guid id)
        {
            return (await _queueDbContext.QueueRecords.FirstOrDefaultAsync(x => x.Id.Equals(id)))!;
        }
       

        public void Update(QueueRecord entity)
        {
            _queueDbContext.QueueRecords.Update(entity);
        }
    }
}
