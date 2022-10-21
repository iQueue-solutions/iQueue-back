using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQueueBL.Interfaces;
using IQueueBL.Models;
using IQueueBL.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IQueueAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private readonly IRecordService _recordService;

        public RecordsController(IRecordService recordService)
        {
            _recordService = recordService;
        }
        

        // GET: api/Records/3bb3e74d-15f8-4efa-bf89-ef5390f9927b
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
        public async Task<ActionResult> Post([FromBody] RecordModel value)
        {
            try
            {
                await _recordService.AddAsync(value);
                return CreatedAtAction(nameof(Get), new { id = value.Id }, value);
            }
            catch (QueueException e)
            {
                return BadRequest($"Exception: {e.Message}");
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
                return BadRequest($"Exception: {e.Message}");
            }
        }
    }
}
