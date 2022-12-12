using IQueueData.Entities;
using IQueueData.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IQueueData.Repositories;

public class SwitchRequestRepository : ISwitchRequestRepository
{
    private readonly QueueDbContext _queueDbContext;

    public SwitchRequestRepository(QueueDbContext queueDbContext)
    {
        _queueDbContext = queueDbContext;
    }
    
    public async Task<IEnumerable<SwitchRequest>> GetAllAsync()
    {
        return await _queueDbContext.SwitchRecords
            .Include(x => x.Record)
            .ThenInclude(y => y.UserQueue)
            .Include(x => x.SwitchWithRecord)
            .ThenInclude(y => y.UserQueue)
            .ToListAsync();
    }

    public async Task<SwitchRequest?> GetByIdAsync(Guid id)
    {
        return await _queueDbContext.SwitchRecords
            .Include(x => x.Record)
            .ThenInclude(y => y.UserQueue)
            .Include(x => x.SwitchWithRecord)
            .ThenInclude(y => y.UserQueue)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Guid> AddAsync(SwitchRequest entity)
    {
        await _queueDbContext.SwitchRecords.AddAsync(entity);
        return entity.Id;
    }

    public void Delete(SwitchRequest entity)
    {
        _queueDbContext.SwitchRecords.Remove(entity); 
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var entity = await _queueDbContext.SwitchRecords.FirstOrDefaultAsync(x => x.Id.Equals(id));
        if (entity != null) _queueDbContext.SwitchRecords.Remove(entity);
    }

    public void Update(SwitchRequest entity)
    {
        _queueDbContext.SwitchRecords.Update(entity);
    }
}