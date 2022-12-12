using AutoMapper;
using IQueueAPI.Requests;
using IQueueBL.Interfaces;
using IQueueBL.Models;
using IQueueBL.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IQueueAPI.Controllers
{
    [Authorize]
    [Route("api/records")]
    [ApiController]
    public class RecordsController : BaseApiController
    {
        private readonly IRecordService _recordService;
        private readonly IMapper _mapper;

        public RecordsController(IRecordService recordService, IMapper mapper)
        {
            _recordService = recordService;
            _mapper = mapper;
        }
        

        // GET: api/Records/3bb3e74d-15f8-4efa-bf89-ef5390f9927b
        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<RecordModel>> Get(Guid id)
        {
            var record =  await _recordService.GetByIdAsync(id);
            if (record == null)
            {
                return NotFound();
            }

            return Ok(record);
        }

        // POST: api/Records
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] RecordPostRequest value)
        {
            try
            {
                var record = _mapper.Map<RecordModel>(value);
                var id = await _recordService.AddAsync(record);
                return Ok(id);
            }
            catch (QueueException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] int newIndex)
        {
            try
            {
                var result = await _recordService.ExchangeRecord(id, newIndex);
                return Ok(result ? "Updated" : "Request created");
            }
            catch (QueueException e)
            {
                return BadRequest(e.Message);
            }
        }
        

        // DELETE: api/Records/3bb3e74d-15f8-4efa-bf89-ef5390f9927b
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await _recordService.DeleteAsync(id);
                return NoContent();
            }
            catch (QueueException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
