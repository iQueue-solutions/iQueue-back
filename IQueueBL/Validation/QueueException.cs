namespace IQueueBL.Validation;

public class QueueException : Exception
{
    public QueueException(string message) : base(message)
    {
    }
}