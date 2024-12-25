using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Application.Todos.Add;
using Todo.Application.Todos.Delete;
using Todo.Application.Todos.GetAll;
using Todo.Application.Todos.ToggleStatus;

namespace Todo.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodosController : ControllerBase
{
    private readonly ISender _sender;

    public TodosController(ISender sender)
    {
        _sender = sender;
    }

    [Authorize]
    [HttpGet]
    [Route("{userId}")]
    public async Task<IActionResult> GetAll([FromRoute] Guid userId)
    {
        var query = new GetAllTodosQuery(userId);
        var res = await _sender.Send(query);
        return res.IsSuccess ? Ok(res.Value) : BadRequest(res.Error);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddTodoRequest todoRequest)
    {
        var command = new AddTodoCommand(todoRequest.Title, todoRequest.Description, todoRequest.UserId);
        var res = await _sender.Send(command);
        return res.IsSuccess ? Ok(res.Value) : BadRequest(res.Error);
    }

    [Authorize]
    [HttpPost]
    [Route("change-status/{todoId}")]
    public async Task<IActionResult> ChangeStatus([FromRoute] Guid todoId,
        [FromBody] ToggleStatusTodoRequest todoRequest)
    {
        var command = new ToggleStatusTodoCommand(todoId, todoRequest.IsCompleted);
        var res = await _sender.Send(command);
        return res.IsSuccess ? Ok(res.Value) : BadRequest(res.Error);
    }

    [Authorize(Policy = "DeletePolicy")]
    [HttpDelete]
    [Route("{todoId}")]
    public async Task<IActionResult> ChangeStatus([FromRoute] Guid todoId)
    {
        var command = new DeleteTodoCommand(todoId);
        var res = await _sender.Send(command);
        return res.IsSuccess ? Ok() : BadRequest(res.Error);
    }
}