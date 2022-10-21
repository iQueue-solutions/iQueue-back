
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
        public Guid? AdminId { get; set; }

        /// <summary>
        /// Time of Queue Creation.
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Queue open status.
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// Maximum users in queue.
        /// </summary>
        public int? MaxRecordNumber { get; set; }

        /// <summary>
        /// List of records of queue changes.
        /// </summary>
        public IList<QueueRecord> QueueRecords { get; set; } = new List<QueueRecord>();
    }
}
