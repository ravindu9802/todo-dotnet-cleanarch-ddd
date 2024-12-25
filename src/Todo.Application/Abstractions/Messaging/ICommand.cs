using MediatR;
using Todo.Domain.Primitives;

namespace Todo.Application.Abstractions.Messaging;

public interface ICommand
    : IRequest<Result>
{
}

public interface ICommand<TResponse>
    : IRequest<Result<TResponse>>
{
}