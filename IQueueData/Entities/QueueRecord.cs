
namespace IQueueData.Entities
{
    /// <summary>
    /// This class represents QueueRecords object.
    /// </summary>
    public class QueueRecord : BaseEntity
    {
        /// <summary>
        /// Number of laboratory work.
        /// </summary>
        public string? LabNumber { get; set; }

        /// <summary>
        /// Index number in queue.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Identificator of created or changed queue.
        /// </summary>
        public Guid QueueId { get; set; }

        /// <summary>
        /// Created or changed queue.
        /// </summary>
        public Queue? Queue  { get; set; }

        /// <summary>
        /// Identificator of entered used.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Entered used.
        /// </summary>
        public User? User { get; set; }
    }
}
