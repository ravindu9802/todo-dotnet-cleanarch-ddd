using FluentValidation;
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
    private readonly IValidator<AddUserRequest> _addUserRequestValidator;

    public UsersController(ISender sender, IValidator<AddUserRequest> addUserRequestValidator)
    {
        _sender = sender;
        _addUserRequestValidator = addUserRequestValidator;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] AddUserRequest userRequest)
    {
        var validationResult = await _addUserRequestValidator.ValidateAsync(userRequest);

        if (!validationResult.IsValid)
        {
            return BadRequest(new { validationError = validationResult.ToDictionary() });
        }

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