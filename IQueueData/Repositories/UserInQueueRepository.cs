using IQueueData.Entities;
using IQueueData.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IQueueData.Repositories;

public class UserInQueueRepository : IUserInQueueRepository
{
    private readonly QueueDbContext _queueDbContext;

    public UserInQueueRepository(QueueDbContext queueDbContext)
    {
        _queueDbContext = queueDbContext;
    }

    public async Task<Guid> AddAsync(UserInQueue entity)
    {
        await _queueDbContext.UsersInQueues.AddAsync(entity);
        return entity.Id;
    }

    public void Delete(UserInQueue entity)
    {
        _queueDbContext.UsersInQueues.Remove(entity);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var entity = await _queueDbContext.UsersInQueues
            .FirstOrDefaultAsync(x => x.Id.Equals(id));
            
        await _queueDbContext.Users.Where(x => x.Id == entity!.UserId).LoadAsync();
        await _queueDbContext.Queues.Where(x => x.Id == entity!.QueueId).LoadAsync();
            
        if (entity != null) _queueDbContext.UsersInQueues.Remove(entity);
    }

    public async Task<IEnumerable<UserInQueue>> GetAllAsync()
    {
        return await _queueDbContext.UsersInQueues.Include(x => x.Queue).ToListAsync();
    }
                

    public async Task<UserInQueue> GetByIdAsync(Guid id)
    {
        return await _queueDbContext
            .UsersInQueues
            .Include(x => x.Queue)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
       

    public void Update(UserInQueue entity)
    {
        _queueDbContext.UsersInQueues.Update(entity);
    }
}