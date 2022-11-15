namespace IQueueAPI.Requests;

public class OpenQueueRequest
{
    public Guid UserId { get; set; }
    public DateTime CloseTime { get; set; }
}