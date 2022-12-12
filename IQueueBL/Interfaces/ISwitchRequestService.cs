using IQueueBL.Models;


namespace IQueueBL.Interfaces;

public interface ISwitchRequestService
{
    public Task<IEnumerable<SwitchRequestModel>> GetSwitchRequests(Guid userId);

    public Task AnswerSwitchRequest(Guid requestId, bool answer);
}