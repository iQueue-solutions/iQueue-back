using IQueueBL.Models;

namespace IQueueBL.Interfaces;

public interface IUserService : ICrud<UserModel>
{
    public Task RegisterAsync(UserModel model, string password);

    public Task<string> LoginAsync(string email, string password);

    public Task UpdatePassword(Guid userId, string currentPassword, string newPassword);

    public Task<IEnumerable<RecordModel>> RecordsByUser(Guid id);

    public Task<IEnumerable<RecordModel>> UpcomingRecordsByUser(Guid id);
}