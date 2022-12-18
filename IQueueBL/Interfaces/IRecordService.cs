using IQueueBL.Models;

namespace IQueueBL.Interfaces;

public interface IRecordService : ICrud<RecordModel>
{
    public Task<bool> ExchangeRecord(Guid recordId, int newIndex);
    public Task<bool> AdminExchangeRecord(Guid recordId, int newIndex);
}