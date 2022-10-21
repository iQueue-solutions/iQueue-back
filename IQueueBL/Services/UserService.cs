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

    public async Task<UserModel> GetByIdAsync(Guid id)
    {
        var user = await _unitOfWork.UserRepository.GetByIdWithDetailsAsync(id);
        return _mapper.Map<UserModel>(user);
    }

    public async Task AddAsync(UserModel model)
    {
        ValidateUser(model);
        
        var user = _mapper.Map<User>(model);
        
        await _unitOfWork.UserRepository.AddAsync(user);
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateAsync(UserModel model)
    {
        ValidateUser(model);

        var user = _mapper.Map<User>(model);

        _unitOfWork.UserRepository.Update(user);
        await _unitOfWork.SaveAsync();

    }

    public async Task DeleteAsync(Guid modelId)    
    {
        if (await GetByIdAsync(modelId) == null)
        {
            throw new QueueException("User wasn't found");
        }
        
        await _unitOfWork.UserRepository.DeleteByIdAsync(modelId);
        await _unitOfWork.SaveAsync();
    }

    private void ValidateUser(UserModel model)
    {
        if (string.IsNullOrEmpty(model.Email))
        {
            throw new QueueException("Email can't be null value.");
        }
        if (string.IsNullOrEmpty(model.FirstName))
        {
            throw new QueueException("FirstName can't be null value.");
        }
        if (string.IsNullOrEmpty(model.LastName))
        {
            throw new QueueException("LastName can't be null value.");
        }
        if (string.IsNullOrEmpty(model.Id.ToString()))
        {
            throw new QueueException("Id can't be null value.");
        }
    }
}