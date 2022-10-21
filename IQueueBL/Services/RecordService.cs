using AutoMapper;
using IQueueBL.Interfaces;
using IQueueBL.Models;
using IQueueBL.Validation;
using IQueueData.Entities;
using IQueueData.Interfaces;

namespace IQueueBL.Services
{
    public class RecordService : IRecordService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RecordService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task AddAsync(RecordModel model)
        {
            ValidateRecord(model);

            var record = _mapper.Map<QueueRecord>(model);

            await _unitOfWork.RecordRepository.AddAsync(record);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(Guid modelId)
        {
            if (await GetByIdAsync(modelId) == null)
            {
                throw new QueueException("Record wasn't found");
            }
            
            await _unitOfWork.RecordRepository.DeleteByIdAsync(modelId);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<RecordModel>> GetAllAsync()
        {
            var records = await _unitOfWork.RecordRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RecordModel>>(records);
        }

        public async Task<RecordModel> GetByIdAsync(Guid id)
        {
            var record = await _unitOfWork.RecordRepository.GetByIdAsync(id);
            return _mapper.Map<RecordModel>(record);
        }

        public async Task UpdateAsync(RecordModel model)
        {
            ValidateRecord(model);

            var record = _mapper.Map<QueueRecord>(model);

            _unitOfWork.RecordRepository.Update(record);
            await _unitOfWork.SaveAsync();
        }


        private async void ValidateRecord(RecordModel model)
        {
            if (string.IsNullOrEmpty(model.QueueId.ToString()))
            {
                throw new QueueException("QueueId can't be null value.");
            }
            if (string.IsNullOrEmpty(model.LabNumber.ToString()))
            {
                throw new QueueException("LabNumber can't be null value.");
            }
            if (string.IsNullOrEmpty(model.Index.ToString()))
            {
                throw new QueueException("Index can't be null value.");
            }
            if (string.IsNullOrEmpty(model.UserId.ToString()))
            {
                throw new QueueException("UserId can't be null value.");
            }
            if (string.IsNullOrEmpty(model.Id.ToString()))
            {
                throw new QueueException("Id can't be null value.");
            }
            
            var queue = await _unitOfWork.QueueRepository.GetByIdWithDetailsAsync(model.QueueId);

            if (model.Index <= 0)
            {
                throw new QueueException("Index must be a positive value."); 
            }

            if (model.LabNumber <= 0)
            {
                throw new QueueException("Task must be a positive value.");
            }
            
            if (queue.QueueRecords.Count >= queue.MaxRecordNumber)
            {
                throw new QueueException("Max records achieved");
            }

            if (queue.QueueRecords.FirstOrDefault(x => x.Index == model.Index) != null)
            {
                throw new QueueException("This place has been already taken");
            }

        }
        
    }
}
