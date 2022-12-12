using AutoMapper;
using IQueueBL.Helpers;
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
    
    
    public async Task RegisterAsync(UserModel model, string password)
    {
        var existingUser = await _unitOfWork.UserRepository.GetByEmailAsync(model.Email);
        if (existingUser != null)
        {
            throw new QueueException("User already exists");
        }
        
        PasswordHelper.CreatePasswordHash(password, out byte[]  passwordHash, out byte[]  passwordSalt);

        var user = new User
        {
            Email = model.Email,
            PasswordHash = Convert.ToBase64String(passwordHash),
            PasswordSalt = Convert.ToBase64String(passwordSalt),
            FirstName = model.FirstName,
            LastName = model.LastName,
        };

        await _unitOfWork.UserRepository.AddAsync(user);

        await _unitOfWork.SaveAsync();
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        var user = await _unitOfWork.UserRepository.GetByEmailAsync(email);

        if (user == null)
        {
            throw new QueueException("Email or password not corresponds.");
        }
        
        var verify = PasswordHelper.VerifyPasswordHash(password, 
            Convert.FromBase64String(user.PasswordHash), 
            Convert.FromBase64String(user.PasswordSalt));

        if (!verify)
        {
            throw new QueueException("Email or password not corresponds.");
        }

        var token = await TokenHelper.GenerateAccessToken(user.Id);

        return token;
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

    public async Task<Guid> AddAsync(UserModel model)
    {
        ValidateUser(model);
        
        var user = _mapper.Map<User>(model);
        
        var id = await _unitOfWork.UserRepository.AddAsync(user);
        await _unitOfWork.SaveAsync();

        return id;
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