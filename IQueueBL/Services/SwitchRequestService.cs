using AutoMapper;
using IQueueBL.Interfaces;
using IQueueBL.Models;
using IQueueData.Entities;
using IQueueData.Interfaces;

namespace IQueueBL.Services;

public class SwitchRequestService : ISwitchRequestService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SwitchRequestService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SwitchRequestModel>> GetSwitchRequests(Guid userId)
    {
        var requests = await _unitOfWork.SwitchRequestRepository.GetAllAsync();

        var users = requests.Where(x => x.SwitchWithRecord?.UserQueue?.UserId == userId);

        var notConfirmed = users.Where(x => x.IsAccepted is null);

        return _mapper.Map<IEnumerable<SwitchRequestModel>>(notConfirmed);
    }

    public async Task AnswerSwitchRequest(Guid requestId, bool answer)
    {
        var request = await _unitOfWork.SwitchRequestRepository.GetByIdAsync(requestId);
        if (request == null) return;
        
        request.IsAccepted = answer;
        await _unitOfWork.SaveAsync();
        
        if (request.IsAccepted == true && request.Record != null && request.SwitchWithRecord != null)
        {
            (request.Record.Index, request.SwitchWithRecord.Index) = 
                (request.SwitchWithRecord.Index, request.Record.Index);
            await _unitOfWork.SaveAsync();
        }
    }

}