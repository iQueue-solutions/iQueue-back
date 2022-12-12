using IQueueData.Entities;

namespace IQueueData.Interfaces
{
    public interface IRecordRepository : IRepository<Record>
    {
        public Task<IEnumerable<Record>> GetAllWithDetailsAsync();
        public void ExchangePlaces(Guid ParticipantId1, Guid ParticipantId2);
    }
}
