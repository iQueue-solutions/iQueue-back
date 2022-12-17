namespace IQueueBL.Models;

/// <summary>
/// Record in queue model.
/// </summary>
public class RecordModel
{
    /// <summary>
    /// Model id.
    /// </summary>
    public Guid Id { get; set; }

    
    /// <summary>
    /// User In queue Id.
    /// </summary>
    public Guid ParticipantId { get; set; }
    
    /// <summary>
    /// User Id.
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Queue Id.
    /// </summary>
    public Guid QueueId { get; set; }

    /// <summary>
    /// Number of laboratory work.
    /// </summary>
    public string? LabNumber { get; set; }

    /// <summary>
    /// Index number in queue.
    /// </summary>
    public int Index { get; set; }
    
    /// <summary>
    /// Start of defense.
    /// </summary>
    public DateTime? StartTime { get; set; }
        
    /// <summary>
    /// Finish of defense.
    /// </summary>
    public DateTime? FinishTime { get; set; }
    
}