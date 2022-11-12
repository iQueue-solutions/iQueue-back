using IQueueBL.Models;

namespace IQueueBL.Interfaces;

public interface IQueueService : ICrud<QueueModel>
{  

    public Task<ICollection<ParticipantModel>> GetParticipantsIds(Guid queueId);

    public Task<bool> Open(Guid queueId, Guid userId, DateTime closeTime);
}