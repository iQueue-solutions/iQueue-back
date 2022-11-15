namespace IQueueAPI.Requests;

public class RecordPostRequest
{

    /// <summary>
    /// Id of created or changed queue.
    /// </summary>
    public Guid ParticipantId { get; set; }


    /// <summary>
    /// Number of laboratory work.
    /// </summary>
    public string? LabNumber { get; set; }

    /// <summary>
    /// Index number in queue.
    /// </summary>
    public int Index { get; set; }

}