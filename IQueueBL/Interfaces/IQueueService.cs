using IQueueBL.Models;

namespace IQueueBL.Interfaces;

public interface IQueueService : ICrud<QueueModel>
{   
    public Task<bool> Open(Guid queueId, Guid userId, DateTime closeTime);
}