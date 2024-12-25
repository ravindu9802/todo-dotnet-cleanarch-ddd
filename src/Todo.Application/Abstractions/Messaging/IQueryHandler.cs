using MediatR;
using Todo.Domain.Primitives;

namespace Todo.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery>
    : IRequestHandler<TQuery, Result>
    where TQuery : IQuery
{
}

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}