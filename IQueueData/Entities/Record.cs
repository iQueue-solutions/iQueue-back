
namespace IQueueData.Entities
{
    /// <summary>
    /// This class represents QueueRecords object.
    /// </summary>
    public class Record : BaseEntity
    {
        /// <summary>
        /// Id of User in queue.
        /// </summary>
        public Guid UserQueueId { get; set; }
        
        /// <summary>
        /// User in queue.
        /// </summary>
        public UserInQueue? UserQueue { get; set; }
        
        /// <summary>
        /// Number of laboratory work.
        /// </summary>
        public string? LabNumber { get; set; }

        /// <summary>
        /// Index number in queue.
        /// </summary>
        public int Index { get; set; }
        
        
        public ICollection<SwitchRequest>? SwitchRequests { get; set; }
        
        public ICollection<SwitchRequest>? MentionedInRequests { get; set; }
    }
}
