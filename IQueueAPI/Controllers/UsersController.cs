using AutoMapper;
using IQueueAPI.Models;
using IQueueBL.Interfaces;
using IQueueBL.Models;
using IQueueBL.Validation;
using Microsoft.AspNetCore.Mvc;

namespace IQueueAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        
        // GET: api/Users
        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> Get()
        {
            return Ok(await _userService.GetAllAsync());
        }

        // GET: api/Users/3bb3e74d-15f8-4efa-bf89-ef5390f9927b
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserModel>> Get(Guid id)
        {
            var user =  await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserPostViewModel value)
        {
            try
            {
                var user = _mapper.Map<UserModel>(value);
                var id = await _userService.AddAsync(user);
                return CreatedAtAction(nameof(Get), id );
            }
            catch (QueueException e)
            {
                return BadRequest($"Exception: {e.Message}");
            }
        }

        // PUT: api/Users/3bb3e74d-15f8-4efa-bf89-ef5390f9927b
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] UserModel value)
        {
            if (id != value.Id) return BadRequest();
            
            try
            {
                await _userService.UpdateAsync(value);
                return Ok(value);
            }
            catch (QueueException e)
            {
                return BadRequest($"Exception: {e.Message}");
            }
        }

        // DELETE: api/Users/3bb3e74d-15f8-4efa-bf89-ef5390f9927b
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await _userService.DeleteAsync(id);
                return NoContent();
            }
            catch (QueueException e)
            {
                return BadRequest($"Exception: {e.Message}");
            }
        }
    }
}
