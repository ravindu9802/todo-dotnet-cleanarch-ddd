using MediatR;
using Todo.Domain.Primitives;

namespace Todo.Application.Abstractions.Messaging;

public interface IQuery
    : IRequest<Result>
{
}

public interface IQuery<TResponse>
    : IRequest<Result<TResponse>>
{
}
