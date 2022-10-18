
namespace IQueueData.Entities
{
    /// <summary>
    /// This class represents Queue object.
    /// </summary>
    public class Queue : BaseEntity
    {
        /// <summary>
        /// Admin User of Queue.
        /// </summary>
        public User? Admin { get; set; }

        /// <summary>
        /// Time of Queue Creation.
        /// </summary>
        public DateTime CreateTime { get; set; }

        public bool IsOpen { get; set; }
        
        public int MaxRecordNumber { get; set; }

        public IList<QueueRecord> QueueRecords { get; set; } = new List<QueueRecord>();
    }
}
