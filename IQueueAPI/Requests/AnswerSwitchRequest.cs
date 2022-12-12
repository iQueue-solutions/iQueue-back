namespace IQueueAPI.Requests;

public class AnswerSwitchRequest
{
    public Guid RequestId { get; set; }
    
    public bool Answer { get; set; }
}