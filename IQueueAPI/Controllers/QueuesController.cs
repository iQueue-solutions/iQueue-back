using IQueueBL.Interfaces;
using IQueueBL.Models;
using IQueueBL.Validation;
using Microsoft.AspNetCore.Mvc;

namespace IQueueAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueuesController : ControllerBase
    {
        private readonly IQueueService _queueService;
        
        // GET: api/Queues
        public QueuesController(IQueueService queueService)
        {
            _queueService = queueService;
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
            var queue =  await _queueService.GetByIdAsync(id);
            if (queue == null)
            {
                return NotFound();
            }

            return Ok(queue);
        }

        // POST: api/Queues
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] QueueModel value)
        {
            try
            {
                await _queueService.AddAsync(value);
                return CreatedAtAction(nameof(Get), new { id = value.Id }, value);
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
    }
}
