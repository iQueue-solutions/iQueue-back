using AutoMapper;
using IQueueBL.Interfaces;
using IQueueBL.Models;
using IQueueBL.Validation;
using IQueueData.Entities;
using IQueueData.Interfaces;

namespace IQueueBL.Services;

public class ParticipantService : IParticipantService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ParticipantService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Guid> AddAsync(ParticipantModel model)
    {
        if ((await _unitOfWork.UserInQueueRepository.GetAllAsync()).FirstOrDefault(x =>
                x.QueueId == model.QueueId && x.UserId == model.UserId) != null)
        {
            throw new QueueException("User already in queue.");
        }
            
        var participant = _mapper.Map<UserInQueue>(model);
        var id = await _unitOfWork.UserInQueueRepository.AddAsync(participant);
        await _unitOfWork.SaveAsync();
        return id;
    }

    public async Task<ErrorModel> AddUsersInQueueAsync(Guid queueId, IEnumerable<Guid> usersIds)
    {
        var result = new ErrorModel { Success = true, Errors = new List<string>() };
        foreach (var userId in usersIds)
        {
            var userInQueue = (await _unitOfWork.UserInQueueRepository.GetAllAsync())
                .FirstOrDefault(x => x.QueueId == queueId && x.UserId == userId);
            if (userInQueue != null)
            {
                result.Success = false;
                result.Errors.Add($"User {userInQueue.UserId} is already in queue {userInQueue.QueueId}");
                continue;
            }

            userInQueue = new UserInQueue { UserId = userId, QueueId = queueId };
            await _unitOfWork.UserInQueueRepository.AddAsync(userInQueue);
            await _unitOfWork.SaveAsync();
        }

        return result;
    }

    public async Task DeleteAsync(Guid modelId)
    {
        await _unitOfWork.UserInQueueRepository.DeleteByIdAsync(modelId);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteParticipantsAsync(Guid participantId, Guid adminId)
    {
        var participant = await _unitOfWork.UserInQueueRepository.GetByIdAsync(participantId);
        if (participant != null && participant.Queue?.AdminId == adminId)
        {
            await DeleteAsync(participantId);
        }
    }

    public async Task<IEnumerable<ParticipantModel>> GetAllAsync()
    {
        var participants = await _unitOfWork.UserInQueueRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ParticipantModel>>(participants);
    }

    public async Task<ParticipantModel> GetByIdAsync(Guid id)
    {
        var participants = await _unitOfWork.UserInQueueRepository.GetByIdAsync(id);
        return _mapper.Map<ParticipantModel>(participants);
    }

    public async Task<ICollection<ParticipantModel>> GetParticipantsIds(Guid queueId)
    {
        var participants = (await _unitOfWork.UserInQueueRepository.GetAllAsync())
            .Where(x => x.QueueId == queueId);

        var result = new List<ParticipantModel>();
        foreach (var participant in participants)
        {
            result.Add(new ParticipantModel { Id = participant.Id, UserId = participant.UserId });
        }

        return result;
    }

    public async Task UpdateAsync(ParticipantModel model)
    {
        var participant = _mapper.Map<UserInQueue>(model);

        _unitOfWork.UserInQueueRepository.Update(participant);
        await _unitOfWork.SaveAsync();
    }
        
}