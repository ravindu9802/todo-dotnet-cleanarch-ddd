using Todo.Application.Abstractions.Messaging;
using Todo.Domain.Entities;
using Todo.Domain.Primitives;
using Todo.Domain.Repositories;

namespace Todo.Application.Users.Add;

internal class AddUserCommandHandler : ICommandHandler<AddUserCommand, Guid>
{
    private readonly IUserUoW _uoW;
    private readonly IUserRepository _userRepository;

    public AddUserCommandHandler(IUserRepository userRepository, IUserUoW uoW)
    {
        _userRepository = userRepository;
        _uoW = uoW;
    }

    public async Task<Result<Guid>> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var result = User.Create(request.FirstName, request.LastName, request.Email, request.Role);

        if (result.IsFailure) return Result.Failure<Guid>(result.Error);

        await _userRepository.CreateUserAsync(result.Value, cancellationToken);
        await _uoW.SaveChangesAsync(cancellationToken);

        return Result.Success(result.Value.Id);
    }
}