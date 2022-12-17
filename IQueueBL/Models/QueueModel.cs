namespace IQueueBL.Models;

/// <summary>
/// Queue to sign up model.
/// </summary>
public class QueueModel
{  
    /// <summary>
    /// Model id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Name of Queue.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Admin User of Queue.
    /// </summary>
    public Guid? AdminId { get; set; }

    /// <summary>
    /// Time of Queue Creation.
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Time of Queue opening.
    /// </summary>
    public DateTime? OpenTime { get; set; }
        
    /// <summary>
    /// Time of Queue closing.
    /// </summary>
    public DateTime? CloseTime { get; set; }
    
    /// <summary>
    /// Time in minutes of lab defense.
    /// </summary>
    public int? LabInterval { get; set; }
    
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
    public IList<ParticipantModel>? Participants { get; set; }
}