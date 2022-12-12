using AutoMapper;
using IQueueAPI.Requests;
using IQueueBL.Interfaces;
using IQueueBL.Models;
using IQueueBL.Validation;
using Microsoft.AspNetCore.Mvc;

namespace IQueueAPI.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public AuthController(IMapper mapper, IUserService userService)
    {
        _mapper = mapper;
        _userService = userService;
    }

    /// <summary>
    /// POST api/auth/register
    /// </summary>
    [HttpPost("register")] 
    public async Task<IActionResult> Register([FromBody] UserRegisterRequest register)
    {
        if (!ModelState.IsValid) return UnprocessableEntity(register);
        
        var user = _mapper.Map<UserModel>(register);

        try
        {
            await _userService.RegisterAsync(user, register.Password);
            return NoContent();
        }
        catch (QueueException e)
        {
            return BadRequest(e.Message);
        }
    }

    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest loginRequest)
    {
        if (!ModelState.IsValid) return UnprocessableEntity(loginRequest);
        
        try
        {
            var token = await _userService.LoginAsync(loginRequest.Email, loginRequest.Password);
            return Ok(token);
        }
        catch (QueueException exception)
        {
            return BadRequest(exception.Message);
        }
    }
}