using MediatR;
using Microsoft.AspNetCore.Mvc;
using Todo.Application.Users.Add;
using Todo.Application.Users.Login;

namespace Todo.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] AddUserRequest userRequest)
    {
        var command = new AddUserCommand(userRequest.FirstName, userRequest.LastName, userRequest.Email, userRequest.Role);
        var res = await _sender.Send(command);
        return res.IsSuccess ? Ok(res.Value) : BadRequest(res.Error);
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest userRequest)
    {
        var command = new UserLoginCommand(userRequest.Email);
        var res = await _sender.Send(command);
        return res.IsSuccess ? Ok(new UserLoginResponse(res.Value)) : BadRequest(res.Error);
    }
}