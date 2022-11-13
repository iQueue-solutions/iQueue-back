using AutoMapper;
using IQueueBL.Interfaces;
using IQueueBL.Models;
using IQueueBL.Validation;
using IQueueData.Entities;
using IQueueData.Interfaces;
using Queue = IQueueData.Entities.Queue;

namespace IQueueBL.Services
{
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
            ValidateParticipant(model);

            var participant = _mapper.Map<UserInQueue>(model);
            var id = await _unitOfWork.UserInQueueRepository.AddAsync(participant);
            await _unitOfWork.SaveAsync();
            return id;
        }

        public async Task AddUsersInQueueAsync(Guid queueId, IEnumerable<Guid> usersIds)
        {
            foreach (var userId in usersIds)
            {
                var userInQueue = (await _unitOfWork.UserInQueueRepository.GetAllAsync())
                    .FirstOrDefault(x => x.QueueId == queueId && x.UserId == userId);
                if (userInQueue != null)
                {
                    continue;
                }

                userInQueue = new UserInQueue { UserId = userId, QueueId = queueId };
                await _unitOfWork.UserInQueueRepository.AddAsync(userInQueue);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task DeleteAsync(Guid modelId)
        {
            if (await GetByIdAsync(modelId) == null)
            {
                throw new ParticipantException("User wasn't found");
            }

            await _unitOfWork.UserInQueueRepository.DeleteByIdAsync(modelId);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteUsersFromQueueAsync(Guid queueId, IEnumerable<Guid> usersIds)
        {
            foreach (var userId in usersIds)
            {
                var userInQueue = (await _unitOfWork.UserInQueueRepository.GetAllAsync())
                    .FirstOrDefault(x => x.QueueId == queueId && x.UserId == userId);

                if (userInQueue != null) await _unitOfWork.UserInQueueRepository.DeleteByIdAsync(userInQueue.Id);
            }
            await _unitOfWork.SaveAsync();
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
            ValidateParticipant(model);

            var participant = _mapper.Map<UserInQueue>(model);

            _unitOfWork.UserInQueueRepository.Update(participant);
            await _unitOfWork.SaveAsync();
        }

        private void ValidateParticipant(ParticipantModel model)
        {
            if (string.IsNullOrEmpty(model.Id.ToString()))
            {
                throw new ParticipantException("Id can't be null value.");
            }
            if (string.IsNullOrEmpty(model.UserId.ToString()))
            {
                throw new ParticipantException("UserId can't be null value.");
            }
            if (string.IsNullOrEmpty(model.QueueId.ToString()))
            {
                throw new ParticipantException("QueueId can't be null value.");
            }           

        }
    }
}
