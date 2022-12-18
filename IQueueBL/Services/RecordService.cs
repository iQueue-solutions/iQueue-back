using AutoMapper;
using IQueueBL.Interfaces;
using IQueueBL.Models;
using IQueueBL.Validation;
using IQueueData.Entities;
using IQueueData.Interfaces;

namespace IQueueBL.Services;

public class RecordService : IRecordService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RecordService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
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
        await ValidateRecord(model);

        var record = _mapper.Map<Record>(model);

        _unitOfWork.RecordRepository.Update(record);
        await _unitOfWork.SaveAsync();
    }
        
        
    public async Task<Guid> AddAsync(RecordModel model)
    {
        await ValidateRecord(model);

        var record = _mapper.Map<Record>(model);

        var id = await _unitOfWork.RecordRepository.AddAsync(record);
        await _unitOfWork.SaveAsync();

        return id;
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


    public async Task<bool> ExchangeRecord(Guid recordId, int newIndex)
    {
        var record = await _unitOfWork.RecordRepository.GetByIdAsync(recordId);
        if (record == null)
        {
            throw new QueueException("Record not found");
        }

        var records = await _unitOfWork.RecordRepository.GetAllWithDetailsAsync();
        var newPlace = records.FirstOrDefault(x =>
            x.UserQueue?.Queue?.Id == record.UserQueue?.Queue?.Id && x.Index == newIndex);
        
        // Case when wanted place is empty
        if (newPlace == null)
        {
            record.Index = newIndex;
            await _unitOfWork.SaveAsync();
            return true;
        }
        
        // Case when wanted place is self
        if (newPlace.UserQueueId == record.UserQueueId)
        {
            newPlace.Index = record.Index;
            record.Index = newIndex;
            _unitOfWork.RecordRepository.Update(record);
            _unitOfWork.RecordRepository.Update(newPlace);
            await _unitOfWork.SaveAsync();
            return true;
        }
        
        // Case when wanted place is another
        var switchRequest = new SwitchRequest
        {
            RecordId = record.Id,
            SwitchWithRecordId = newPlace.Id
        };

        await _unitOfWork.SwitchRequestRepository.AddAsync(switchRequest);
        await _unitOfWork.SaveAsync();
        return false;
    }

    public async Task AdminExchangeRecord(Guid recordId, int newIndex, Guid userId)
    {
        var record = await _unitOfWork.RecordRepository.GetByIdAsync(recordId);
        if (record == null)
        {
            throw new QueueException("Record not found");
        }

        var queue = await _unitOfWork.QueueRepository.GetByIdAsync(record.UserQueue!.QueueId);
        if (queue != null && queue.AdminId != userId)
        {
            throw new QueueException("Not admin of queue.");
        }

        var records = await _unitOfWork.RecordRepository.GetAllWithDetailsAsync();
        var newPlace = records.FirstOrDefault(x =>
            x.UserQueue?.Queue?.Id == record.UserQueue?.Queue?.Id && x.Index == newIndex);

        // Case when wanted place is empty
        if (newPlace == null)
        {
            record.Index = newIndex;
            await _unitOfWork.SaveAsync();
            return;
        }

        // Case when wanted place is another
        (record.Index, newPlace.Index) = (newPlace.Index, record.Index);
        _unitOfWork.RecordRepository.Update(record);
        _unitOfWork.RecordRepository.Update(newPlace);
        await _unitOfWork.SaveAsync();
    }

    private async Task ValidateRecord(RecordModel model)
    {
        if (model.Index < 0)
        {
            throw new QueueException("Index must be a positive value.");
        }

        var userInQueue = await _unitOfWork.UserInQueueRepository.GetByIdAsync(model.ParticipantId);
        if (userInQueue == null)
        {
            throw new QueueException("Participant can't be null value.");
        } 

        var queue = await _unitOfWork.QueueRepository.GetByIdWithDetailsAsync(userInQueue.QueueId);

        var queueRecords = (await _unitOfWork.RecordRepository.GetAllWithDetailsAsync())
            .Where(x => x.UserQueue?.QueueId == queue.Id)
            .ToList();

        if (model.Index >= queue.MaxRecordNumber)
        {
            throw new QueueException($"Index must be less than {queue.MaxRecordNumber} for this queue.");
        }
        
        if (queueRecords.FirstOrDefault(x => x.Index == model.Index) != null)
        {
            throw new QueueException("This place has been already taken");
        }
    }
}