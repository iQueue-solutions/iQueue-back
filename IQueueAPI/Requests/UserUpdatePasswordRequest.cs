namespace IQueueAPI.Requests;

public class UserUpdatePasswordRequest
{
    public string CurrentPassword { get; set; } = string.Empty;

    public string NewPassword { get; set; } = string.Empty;
}