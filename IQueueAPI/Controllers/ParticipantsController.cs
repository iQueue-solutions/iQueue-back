using AutoMapper;
using IQueueAPI.Requests;
using IQueueBL.Interfaces;
using IQueueBL.Models;
using IQueueBL.Validation;
using Microsoft.AspNetCore.Mvc;

namespace IQueueAPI.Controllers
{
    [Route("api/participants")]
    [ApiController]
    public class ParticipantsController : ControllerBase
    {
        private readonly IParticipantService _participantService;
        private readonly IMapper _mapper;

        // GET: api/Participants
        public ParticipantsController(IParticipantService participantService, IMapper mapper)
        {
            _participantService = participantService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParticipantModel>>> Get()
        {
            return Ok(await _participantService.GetAllAsync());
        }

        // GET: api/Participants/3bb3e74d-15f8-4efa-bf89-ef5390f9927b
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ParticipantModel>> Get(Guid id)
        {
            var participant = await _participantService.GetByIdAsync(id);
            if (participant == null)
            {
                return NotFound();
            }

            return Ok(participant);
        }

        // POST: api/Participants
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ParticipantPostRequest value)
        {
            try
            {
                var participant = _mapper.Map<ParticipantModel>(value);                
                var id = await _participantService.AddAsync(participant);
                return Ok(id);
            }
            catch (QueueException e)
            {
                return BadRequest($"Exception: {e.Message}");
            }
        }

        [HttpPost("collection")]
        public async Task<ActionResult> PostCollection(Guid queueId, IEnumerable<Guid> userIds)
        {
            var result = await _participantService.AddUsersInQueueAsync(queueId, userIds);
            if (!result.Success)
                return BadRequest(result.Errors);

            return Ok();
        }


        // PUT: api/Queues/3bb3e74d-15f8-4efa-bf89-ef5390f9927b
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] ParticipantModel value)
        {
            if (id != value.Id) return BadRequest();

            try
            {
                await _participantService.UpdateAsync(value);
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
                await _participantService.DeleteAsync(id);
                return NoContent();
            }
            catch (QueueException e)
            {
                return BadRequest($"Exception: {e.Message}");
            }
        }
        
        #region Check if nessessary
        // [HttpPost("{id:guid}/add-participants")]
        // public async Task<ActionResult> AddUsersInQueue(Guid id, [FromBody] IEnumerable<Guid> usersIds)
        // {
        //     try
        //     {
        //         await _participantService.AddUsersInQueueAsync(id, usersIds);
        //         return Ok();
        //     }
        //     catch (ParticipantException e)
        //     {
        //         return BadRequest($"Exception: {e.Message}");
        //     }
        // }
        //
        // [HttpDelete("{id:guid}/delete-participants")]
        // public async Task<ActionResult> DeleteUsersFromQueue(Guid id, [FromBody] IEnumerable<Guid> usersIds)
        // {
        //     try
        //     {
        //         await _participantService.DeleteUsersFromQueueAsync(id, usersIds);
        //         return Ok();
        //     }
        //     catch (QueueException e)
        //     {
        //         return BadRequest($"Exception: {e.Message}");
        //     }
        // }
        //
        // [HttpGet("{id:guid}/participants")]
        // public async Task<ActionResult> GetQueueParticipantsIds(Guid id)
        // {
        //     return Ok(await _participantService.GetParticipantsIds(id));
        // }
        
        #endregion
    }
}
