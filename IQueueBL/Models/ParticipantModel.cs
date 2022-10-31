namespace IQueueBL.Models;

public class ParticipantModel
{
    /// <summary>
    /// Model id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Id of entered used.
    /// </summary>
    public Guid UserId { get; set; }
}