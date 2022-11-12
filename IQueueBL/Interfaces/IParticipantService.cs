using IQueueBL.Models;

namespace IQueueBL.Interfaces;

public interface IParticipantService
{
    public Task AddUsersInQueueAsync(Guid queueId, IEnumerable<Guid> usersIds);

    public Task DeleteUsersFromQueueAsync(Guid queueId, IEnumerable<Guid> usersIds);
}