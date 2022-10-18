
namespace IQueueData.Entities
{
    public class QueueRecord : BaseEntity
    {
        public int LabNumber { get; set; }

        public int Index { get; set; }

        public Guid QueueId { get; set; }

        public Queue? Queue  { get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
