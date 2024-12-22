using MediatR;
using Todo.Domain.Abstractions;
using Todo.Domain.Primitives;
using Todo.Domain.Repositories;

namespace Todo.Application.Users.Login;

internal class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, Result<string>>
{
    private readonly IUserRepository _repository;
    private readonly IJwtProvider _jwtProvider;

    public UserLoginCommandHandler(IUserRepository repository, IJwtProvider jwtProvider)
    {
        _repository = repository;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<string>> Handle(UserLoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetUserByEmailAsync(request.Email, cancellationToken);

        if (user is null) return Result.Failure<string>(new Error("User.NotFound", $"Todo for email {request.Email} not found."));

        bool validLogin = user.Login(request.Email);

        if (!validLogin) return Result.Failure<string>(new Error("User.InvalidCredentials", "Invalid Email or Password."));

        var token = _jwtProvider.Generate(user);

        return Result.Success(token);
    }
}
