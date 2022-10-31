﻿using IQueueBL.Models;

namespace IQueueBL.Interfaces;

public interface IQueueService : ICrud<QueueModel>
{
    public Task AddUsersInQueueAsync(Guid queueId, IEnumerable<Guid> usersIds);

    public Task DeleteUsersFromQueueAsync(Guid queueId, IEnumerable<Guid> usersIds);

    public Task<ICollection<ParticipantModel>> GetParticipantsIds(Guid queueId);
}