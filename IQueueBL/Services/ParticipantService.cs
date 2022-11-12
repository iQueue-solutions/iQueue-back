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
    }
}
