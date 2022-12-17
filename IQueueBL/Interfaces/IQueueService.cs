using IQueueBL.Models;

namespace IQueueBL.Interfaces;

public interface IQueueService : ICrud<QueueModel>
{   
    public Task Open(Guid queueId, Guid userId);
    
    public Task Close(Guid queueId, Guid userId);

    public Task<ICollection<RecordModel>> GetRecordsInQueue(Guid queueId);

    public Task DeleteAsync(Guid queueId, Guid userId);
    
    public Task TransferAdminStatus(Guid queueId, Guid newAdminId, Guid userId);
}