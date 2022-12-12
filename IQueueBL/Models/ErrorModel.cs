namespace IQueueBL.Models;

public class ErrorModel
{
    public bool Success { get; set; }
    
    public ICollection<string>? Errors { get; set; }
}