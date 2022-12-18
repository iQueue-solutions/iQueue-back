namespace IQueueBL.Models;

/// <summary>
/// Model of user.
/// </summary>
public class UserModel
{
    /// <summary>
    /// Model id.
    /// </summary>
    public Guid Id { get; set; }
    
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
    public string Email { get; set; } = string.Empty;
}