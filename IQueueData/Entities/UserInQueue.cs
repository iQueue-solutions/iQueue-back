namespace IQueueData.Entities;

public class UserInQueue : BaseEntity
{
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