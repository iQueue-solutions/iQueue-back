using IQueueBL.Models;

namespace IQueueBL.Interfaces;

public interface IUserService : ICrud<UserModel>
{
    public Task RegisterAsync(UserModel model, string password);

    public Task<string> LoginAsync(string email, string password);
}