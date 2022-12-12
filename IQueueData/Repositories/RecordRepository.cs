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

        public async Task<Guid> AddAsync(Record entity)
        {
            await _queueDbContext.Records.AddAsync(entity);
            return entity.Id;
        }

        public void Delete(Record entity)
        {
            _queueDbContext.Records.Remove(entity);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var entity = await _queueDbContext.Records
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
            
           await _queueDbContext.UserQueueCollection.Where(x => x.Id == entity!.UserQueueId).LoadAsync();
            
           if (entity != null) _queueDbContext.Records.Remove(entity);
        }

        

        public async Task<IEnumerable<Record>> GetAllAsync()
        {
            var result = await _queueDbContext.Records.ToListAsync();
            return result;
        }
        
        
        public async Task<IEnumerable<Record>> GetAllWithDetailsAsync()
        {
            var result = await _queueDbContext.Records
                .Include(x => x.UserQueue)
                .ToListAsync();
            return result;
        }   

        public async Task<Record> GetByIdAsync(Guid id)
        {
            return (await _queueDbContext.Records
                .Include(x => x.UserQueue)
                .FirstOrDefaultAsync(x => x.Id == id))!;
        }
       

        public void Update(Record entity)
        {
            _queueDbContext.Records.Update(entity);
        }

        public void ExchangePlaces(Guid ParticipantId1, Guid ParticipantId2)
        {
            var record1 = _queueDbContext.Records.FirstOrDefault(x => x.UserQueueId == ParticipantId1);
            var record2 = _queueDbContext.Records.FirstOrDefault(x => x.UserQueueId == ParticipantId2);
            if (record1 != null && record2 != null)
            {
                int temp = record1.Index;
                record1.Index = record2.Index;
                record2.Index = temp;
                Update(record1);
                Update(record2);
            }
            

        }
    }
}
