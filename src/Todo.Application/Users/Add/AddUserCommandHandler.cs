using MediatR;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;

namespace Todo.Application.Users.Add;

internal class AddUserCommandHandler : IRequestHandler<AddUserCommand, Guid?>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserUoW _uoW;

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
