using IQueueData.Entities;
using IQueueData.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IQueueData.Repositories;

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
            
        await _queueDbContext.UsersInQueues.Where(x => x.Id == entity!.UserQueueId).LoadAsync();
            
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
            .ThenInclude(y => y.Queue)
            .ToListAsync();
        return result;
    }   

    public async Task<Record?> GetByIdAsync(Guid id)
    {
        return (await _queueDbContext.Records
            .Include(x => x.UserQueue)
            .ThenInclude(y => y.Queue)
            .FirstOrDefaultAsync(x => x.Id == id))!;
    }
       

    public void Update(Record entity)
    {
        _queueDbContext.Records.Update(entity);
    }
}