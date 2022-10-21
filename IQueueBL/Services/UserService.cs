using AutoMapper;
using IQueueBL.Interfaces;
using IQueueBL.Models;
using IQueueBL.Validation;
using IQueueData.Entities;
using IQueueData.Interfaces;

namespace IQueueBL.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserModel>> GetAllAsync()
    {
        var users = await _unitOfWork.UserRepository.GetAllWithDetailsAsync();

        return _mapper.Map<IEnumerable<UserModel>>(users);
    }

    public Task<UserModel> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(UserModel model)
    {
        ValidateUser(model);
        
        var user = _mapper.Map<User>(model);
        
        await _unitOfWork.UserRepository.AddAsync(user);
        await _unitOfWork.SaveAsync();
    }

    public Task UpdateAsync(UserModel model)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid modelId)
    {
        throw new NotImplementedException();
    }

    private void ValidateUser(UserModel model)
    {
        if (string.IsNullOrEmpty(model.Email))
        {
            throw new QueueException("Email can't be null value.");
        }
    }
}