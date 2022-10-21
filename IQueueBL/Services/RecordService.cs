using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IQueueBL.Interfaces;
using IQueueBL.Models;
using IQueueBL.Validation;
using IQueueData.Entities;
using IQueueData.Interfaces;

namespace IQueueBL.Services
{
    internal class RecordService : IRecordService
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
            var model = _mapper.Map<RecordModel>(this.GetByIdAsync(modelId));
            ValidateRecord(model);

            var record = _mapper.Map<QueueRecord>(model);

            await _unitOfWork.RecordRepository.DeleteByIdAsync(record.Id);
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
        private void ValidateRecord(RecordModel model)
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

        }
    }
}
