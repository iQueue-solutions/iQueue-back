using AutoMapper;
using IQueueAPI.Models;
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

        public ParticipantsController(IParticipantService participantService, IMapper mapper)
        {
            _participantService = participantService;
            _mapper = mapper;
        }

        [HttpPost("{id:guid}/add-participants")]
        public async Task<ActionResult> AddUsersInQueue(Guid id, [FromBody] IEnumerable<Guid> usersIds)
        {
            try
            {
                await _participantService.AddUsersInQueueAsync(id, usersIds);
                return Ok();
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
                await _participantService.DeleteUsersFromQueueAsync(id, usersIds);
                return Ok();
            }
            catch (QueueException e)
            {
                return BadRequest($"Exception: {e.Message}");
            }
        }
    }
}
