using AutoMapper;
using IQueueBL.Interfaces;
using IQueueBL.Models;
using IQueueBL.Validation;
using IQueueData.Interfaces;
using Queue = IQueueData.Entities.Queue;

namespace IQueueBL.Services
{
    public class QueueService : IQueueService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public QueueService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<Guid> AddAsync(QueueModel model)
        {
            ValidateQueue(model);

            var queue = _mapper.Map<Queue>(model);
            var id = await _unitOfWork.QueueRepository.AddAsync(queue);
            await _unitOfWork.SaveAsync();
            return id;
        }

        public async Task DeleteAsync(Guid modelId)
        {
            if (await GetByIdAsync(modelId) == null)
            {
                throw new QueueException("User wasn't found");
            }

            await _unitOfWork.QueueRepository.DeleteByIdAsync(modelId);
            await _unitOfWork.SaveAsync();

        }

        public async Task<IEnumerable<QueueModel>> GetAllAsync()
        {
            var queues = await _unitOfWork.QueueRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<QueueModel>>(queues);
        }

        public async Task DeleteAsync(Guid queueId, Guid userId)
        {
            var queue = await _unitOfWork.QueueRepository.GetByIdWithDetailsAsync(queueId);

            if (queue == null)
            {
                throw new QueueException("Queue not found");
            }
            if (queue.AdminId != userId)
            {
                throw new QueueException("Not admin of queue.");
            }
            
            await _unitOfWork.QueueRepository.DeleteByIdAsync(queueId);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<QueueModel>> GetAllWithParticipantsAsync()
        {
            var queues = await _unitOfWork.QueueRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<QueueModel>>(queues);
        } 

        public async Task<QueueModel> GetByIdAsync(Guid id)
        {
            var queue = await _unitOfWork.QueueRepository.GetByIdWithDetailsAsync(id);
            return _mapper.Map<QueueModel>(queue);
        }

        public async Task UpdateAsync(QueueModel model)
        {
            ValidateQueue(model);

            var queue = _mapper.Map<Queue>(model);

            _unitOfWork.QueueRepository.Update(queue);
            await _unitOfWork.SaveAsync();
        }       

        public async Task Open(Guid queueId, Guid userId)
        {
            var queue = await _unitOfWork.QueueRepository.GetByIdWithDetailsAsync(queueId);

            if (queue == null)
            {
                throw new QueueException("Queue not found");
            }
            if (queue.AdminId != userId)
            {
                throw new QueueException("Not admin of queue.");
            }

            queue.OpenTime = DateTime.Now;
            queue.CloseTime = DateTime.Now + TimeSpan.FromDays(7);
            queue.IsOpen = true;
            await _unitOfWork.SaveAsync();
        }

        public async Task Close(Guid queueId, Guid userId)
        {
            var queue = await _unitOfWork.QueueRepository.GetByIdWithDetailsAsync(queueId);

            if (queue == null)
            {
                throw new QueueException("Queue not found");
            }
            if (queue.AdminId != userId)
            {
                throw new QueueException("Not admin of queue.");
            }
            
            queue.CloseTime = DateTime.Now;
            queue.IsOpen = false;
            await _unitOfWork.SaveAsync();
        }

        public async Task<ICollection<RecordModel>> GetRecordsInQueue(Guid queueId)
        {
            var queue = await _unitOfWork.QueueRepository.GetByIdWithDetailsAsync(queueId);

            var queueRecords = (await _unitOfWork.RecordRepository.GetAllWithDetailsAsync())
                .Where(x => x.UserQueue?.QueueId == queue.Id)
                .ToList();

            return _mapper.Map<ICollection<RecordModel>>(queueRecords);
        }

        private void ValidateQueue(QueueModel model)
        {
            if (string.IsNullOrEmpty(model.CreateTime.ToString()))
            {
                throw new QueueException("CreateTime can't be null value.");
            }
            if (string.IsNullOrEmpty(model.MaxRecordNumber.ToString()))
            {
                throw new QueueException("MaxRecordsNumber can't be null value.");
            }
            if (string.IsNullOrEmpty(model.AdminId.ToString()))
            {
                throw new QueueException("AdminId can't be null value.");
            }
            if (string.IsNullOrEmpty(model.Id.ToString()))
            {
                throw new QueueException("Id can't be null value.");
            }
            if (string.IsNullOrEmpty(model.IsOpen.ToString()))
            {
                throw new QueueException("IsOpen can't be null value.");
            }
        }
    }
}
