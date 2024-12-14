using MediatR;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;

namespace Todo.Application.Users.Add;

internal class AddUserCommandHandler : IRequestHandler<AddUserCommand, Guid?>
{
    private readonly IUserUoW _uoW;
    private readonly IUserRepository _userRepository;

    public AddUserCommandHandler(IUserRepository userRepository, IUserUoW uoW)
    {
        _userRepository = userRepository;
        _uoW = uoW;
    }

    public async Task<Guid?> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(request.FirstName, request.LastName);
        await _userRepository.CreateUserAsync(user, cancellationToken);
        await _uoW.SaveChangesAsync(cancellationToken);
        return user.Id;
    }
}