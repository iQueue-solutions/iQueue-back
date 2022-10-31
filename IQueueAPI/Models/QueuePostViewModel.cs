using IQueueBL.Models;

namespace IQueueAPI.Models;

public class QueuePostViewModel
{
    /// <summary>
    /// Name of Queue.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Admin User of Queue.
    /// </summary>
    public Guid? AdminId { get; set; }
    
    /// <summary>
    /// Time of Queue opening.
    /// </summary>
    public DateTime? OpenTime { get; set; }
        
    /// <summary>
    /// Time of Queue closing.
    /// </summary>
    public DateTime? CloseTime { get; set; }

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
    public IList<Guid>? ParticipantsIds { get; set; }
}