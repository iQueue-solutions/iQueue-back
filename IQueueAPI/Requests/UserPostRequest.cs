namespace IQueueAPI.Requests;

public class UserPostRequest
{
    /// <summary>
    /// First name of the user.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Last name of the user.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Email of the user.
    /// </summary>
    public string? Email { get; set; }
}