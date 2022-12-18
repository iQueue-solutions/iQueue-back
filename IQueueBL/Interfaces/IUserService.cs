using IQueueBL.Models;

namespace IQueueBL.Interfaces;

public interface IUserService : ICrud<UserModel>
{
    public Task RegisterAsync(UserModel model, string password);

    public Task<string> LoginAsync(string email, string password);

    public Task UpdatePassword(Guid userId, string currentPassword, string newPassword);
}