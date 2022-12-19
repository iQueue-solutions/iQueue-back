using AutoMapper;
using IQueueAPI.Requests;
using IQueueBL.Interfaces;
using IQueueBL.Models;
using IQueueBL.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IQueueAPI.Controllers;

[Authorize]
[Route("api/users")]
[ApiController]
public class UsersController : BaseApiController
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
        
    // GET: api/Users
    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserModel>>> Get()
    {
        return Ok(await _userService.GetAllAsync());
    }

    // GET: api/Users/3bb3e74d-15f8-4efa-bf89-ef5390f9927b
    [AllowAnonymous]
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


    [AllowAnonymous]
    [HttpGet("{id:guid}/records")]
    public async Task<ActionResult> GetUserRecords(Guid id, [FromQuery] bool onlyUpcoming)
    {
        var records = onlyUpcoming
            ? await _userService.UpcomingRecordsByUser(id)
            : await _userService.RecordsByUser(id);

        return Ok(records);
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
            return BadRequest(e.Message);
        }
    }


    [HttpPut("{id:guid}/password")]
    public async Task<ActionResult> UpdatePassword(Guid id, [FromBody] UserUpdatePasswordRequest request)
    {
        try
        {
            await _userService.UpdatePassword(id, request.CurrentPassword, request.NewPassword);
            return Ok();
        }
        catch (QueueException e)
        {
            return BadRequest(e.Message);
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
            return BadRequest(e.Message);
        }
    }
}