using IQueueBL.Models;

namespace IQueueBL.Interfaces;

public interface IParticipantService : ICrud<ParticipantModel>
{
    public Task<ErrorModel> AddUsersInQueueAsync(Guid queueId, IEnumerable<Guid> usersIds);

    public Task DeleteParticipantsAsync(IEnumerable<Guid> participants, Guid adminId);

    public Task<ICollection<ParticipantModel>> GetParticipantsIds(Guid queueId);
}