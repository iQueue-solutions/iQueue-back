using AutoMapper;
using IQueueAPI.Requests;
using IQueueBL.Interfaces;
using IQueueData.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IQueueAPI.Controllers;

[Authorize]
[Route("api/switch-requests")]
[ApiController]
public class SwitchRequestsController : BaseApiController
{
    private readonly ISwitchRequestService _switchRequestService;
    private readonly IMapper _mapper;
        

    public SwitchRequestsController(ISwitchRequestService switchRequestService, IMapper mapper)
    {
        _switchRequestService = switchRequestService;
        _mapper = mapper;
    }


    [AllowAnonymous]
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> Get(Guid userId)
    {
        var requests = await _switchRequestService.GetSwitchRequests(userId);
        return Ok(requests);
    }
    
    
    [HttpPut]
    public async Task<IActionResult> Get([FromBody] AnswerSwitchRequest request)
    {
        await _switchRequestService.AnswerSwitchRequest(request.RequestId, request.Answer);
        return NoContent();
    }
}