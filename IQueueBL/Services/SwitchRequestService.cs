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
        var k = await _unitOfWork.SwitchRequestRepository.GetAllAsync();

        var v = k.Where(x => x.SwitchWithRecord?.UserQueue?.UserId == userId);

        var q = v.Where(x => x.IsAccepted is null);

        return _mapper.Map<IEnumerable<SwitchRequestModel>>(q);
    }

    public async Task AnswerSwitchRequest(Guid requestId, bool answer)
    {
        var request = await _unitOfWork.SwitchRequestRepository.GetByIdAsync(requestId);
        if (request != null)
        {
            request.IsAccepted = answer;
        }
    }

}