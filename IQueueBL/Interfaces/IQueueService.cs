using IQueueBL.Models;

namespace IQueueBL.Interfaces;

public interface IQueueService : ICrud<QueueModel>
{   
    public Task<bool> Open(Guid queueId, Guid userId, DateTime closeTime);
    
    public Task<bool> Close(Guid queueId, Guid userId);

    public Task<ICollection<RecordModel>> GetRecordsInQueue(Guid queueId);
}