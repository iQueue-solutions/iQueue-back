namespace IQueueAPI.Models;

public class RecordPostViewModel
{
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
    /// Id of entered used.
    /// </summary>
    public Guid UserId { get; set; }
}