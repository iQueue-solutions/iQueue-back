using AutoMapper;
using IQueueAPI.Requests;
using IQueueBL.Interfaces;
using IQueueBL.Models;
using IQueueBL.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IQueueAPI.Controllers;

[Authorize]
[Route("api/queues")]
[ApiController]
public class QueuesController : BaseApiController
{
    private readonly IQueueService _queueService;
    private readonly IMapper _mapper;

    // GET: api/Queues
    public QueuesController(IQueueService queueService, IMapper mapper)
    {
        _queueService = queueService;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<QueueModel>>> Get()
    {
        return Ok(await _queueService.GetAllAsync());
    }
        
    // GET: api/Queues/3bb3e74d-15f8-4efa-bf89-ef5390f9927b
    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<QueueModel>> Get(Guid id)
    {
        var queue = await _queueService.GetByIdAsync(id);
        if (queue == null)
        {
            return NotFound();
        }

        return Ok(queue);
    }

    [AllowAnonymous]
    [HttpGet("{id:guid}/records")]
    public async Task<ActionResult<ICollection<RecordModel>>> GetRecordsInQueue(Guid id)
    {
        return Ok(await _queueService.GetRecordsInQueue(id));
    }


    // POST: api/Queues
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueuePostRequest value)
    {
        try
        {
            var queue = _mapper.Map<QueueModel>(value);
            queue.CreateTime = DateTime.Now;
            var id = await _queueService.AddAsync(queue);
            return CreatedAtAction(nameof(Get), id);
        }
        catch (QueueException e)
        {
            return BadRequest(e.Message);
        }
    }

    // PUT: api/Queues/3bb3e74d-15f8-4efa-bf89-ef5390f9927b
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] QueueModel value)
    {
        if (id != value.Id) return BadRequest();
        if (UserId != value.AdminId) return BadRequest("Not admin of queue.");
            
        try
        {
            await _queueService.UpdateAsync(value);
            return Ok(value);
        }
        catch (QueueException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id:guid}/open")]
    public async Task<ActionResult> OpenQueue(Guid id)
    {
        try
        {
            await _queueService.Open(id, UserId);
            return Ok("Queue is opened");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
        
        
    [HttpPut("{id:guid}/close")]
    public async Task<ActionResult> CloseQueue(Guid id)
    {
        try
        {
            await _queueService.Close(id, UserId);
            return Ok("Queue is closed");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    
    [HttpPut("{id:guid}/admin-status")]
    public async Task<ActionResult> TransferAdminStatus(Guid id, [FromBody] Guid newAdminId)
    {
        try
        {
            await _queueService.TransferAdminStatus(id, newAdminId, UserId);
            return Ok("Admin changed");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    // DELETE: api/Queues/3bb3e74d-15f8-4efa-bf89-ef5390f9927b
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await _queueService.DeleteAsync(id, UserId);
            return NoContent();
        }
        catch (QueueException e)
        {
            return BadRequest(e.Message);
        }
    }
}