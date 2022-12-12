using System.ComponentModel.DataAnnotations;

namespace IQueueAPI.Requests;

public class UserRegisterRequest
{
    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
    
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
}