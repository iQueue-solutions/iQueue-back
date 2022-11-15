namespace IQueueAPI.Requests
{
    public class ParticipantPostRequest
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
