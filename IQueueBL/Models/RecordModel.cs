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
    /// Number of laboratory work.
    /// </summary>
    public int LabNumber { get; set; }

    /// <summary>
    /// Index number in queue.
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// Id of created or changed queue.
    /// </summary>
    public Guid QueueId { get; set; }

    /// <summary>
    /// Created or changed queue.
    /// </summary>
    public QueueModel? Queue  { get; set; }

    /// <summary>
    /// Id of entered used.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Entered used.
    /// </summary>
    public UserModel? User { get; set; }
}