namespace IQueueAPI.Models
{
    public class ParticipantPostViewModel
    {
        /// <summary>
        /// Id of entered used.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Identificator queue.
        /// </summary>
        public Guid QueueId { get; set; }
    }
}
