using MediatR;
using Microsoft.AspNetCore.Mvc;
using Todo.Application.Users.Add;

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
    public async Task<IActionResult> Create([FromBody] AddUserRequest userRequest)
    {
        var command = new AddUserCommand(userRequest.FirstName, userRequest.LastName);
        var res = await _sender.Send(command);
        return res.IsSuccess ? Ok(res.Value) : BadRequest(res.Error);
    }
}