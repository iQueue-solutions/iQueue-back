using AutoMapper;
using IQueueAPI.Models;
using IQueueBL.Interfaces;
using IQueueBL.Models;
using IQueueBL.Validation;
using Microsoft.AspNetCore.Mvc;

namespace IQueueAPI.Controllers
{
    [Route("api/queues")]
    [ApiController]
    public class QueuesController : ControllerBase
    {
        private readonly IQueueService _queueService;
        private readonly IMapper _mapper;

        // GET: api/Queues
        public QueuesController(IQueueService queueService, IMapper mapper)
        {
            _queueService = queueService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QueueModel>>> Get()
        {
            return Ok(await _queueService.GetAllAsync());
        }

        // GET: api/Queues/3bb3e74d-15f8-4efa-bf89-ef5390f9927b
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

        [HttpGet("{id:guid}/participants")]
        public async Task<ActionResult> GetQueueParticipantsIds(Guid id)
        {
            return Ok(await _queueService.GetParticipantsIds(id));
        }

        // POST: api/Queues
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] QueuePostViewModel value)
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
                return BadRequest($"Exception: {e.Message}");
            }
        }



        [HttpPost("{id:guid}/add-participants")]
        public async Task<ActionResult> AddUsersInQueue(Guid id, [FromBody] IEnumerable<Guid> usersIds)
        {
            try
            {
                await _queueService.AddUsersInQueueAsync(id, usersIds);
                return Ok();
            }
            catch (QueueException e)
            {
                return BadRequest($"Exception: {e.Message}");
            }
        }

        // PUT: api/Queues/3bb3e74d-15f8-4efa-bf89-ef5390f9927b
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] QueueModel value)
        {
            if (id != value.Id) return BadRequest();

            try
            {
                await _queueService.UpdateAsync(value);
                return Ok(value);
            }
            catch (QueueException e)
            {
                return BadRequest($"Exception: {e.Message}");
            }
        }

        [HttpPut("{id:guid}/open")]
        public async Task<ActionResult> OpenQueue(Guid id, [FromBody] OpenQueueRequest request)
        {
            try
            {
                var result = await _queueService.Open(id, request.UserId, request.CloseTime);
                if (result == false)
                    return new EmptyResult();
          
                return Ok( ("Queue is opened", request.CloseTime) );
            }
            catch (Exception e)
            {
                return BadRequest($"Exception: {e.Message}");
            }
        }


        // DELETE: api/Queues/3bb3e74d-15f8-4efa-bf89-ef5390f9927b
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await _queueService.DeleteAsync(id);
                return NoContent();
            }
            catch (QueueException e)
            {
                return BadRequest($"Exception: {e.Message}");
            }
        }
        
        [HttpDelete("{id:guid}/delete-participants")]
        public async Task<ActionResult> DeleteUsersFromQueue(Guid id, [FromBody] IEnumerable<Guid> usersIds)
        {
            try
            {
                await _queueService.DeleteUsersFromQueueAsync(id, usersIds);
                return Ok();
            }
            catch (QueueException e)
            {
                return BadRequest($"Exception: {e.Message}");
            }
        }
    }
}
