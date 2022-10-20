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
            var entity = await _queueDbContext.QueueRecords.FirstOrDefaultAsync(x => x.Id.Equals(id));
            _queueDbContext.QueueRecords.Remove(entity);
            await _queueDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<QueueRecord>> GetAllAsync()
        {
            var result = await _queueDbContext.QueueRecords.ToListAsync();
            return result;
        }
                

        public async Task<QueueRecord> GetByIdAsync(Guid id)
        {
            return await _queueDbContext.QueueRecords.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }
       

        public void Update(QueueRecord entity)
        {
            _queueDbContext.QueueRecords.Update(entity);
            _queueDbContext.SaveChanges();
        }
    }
}
