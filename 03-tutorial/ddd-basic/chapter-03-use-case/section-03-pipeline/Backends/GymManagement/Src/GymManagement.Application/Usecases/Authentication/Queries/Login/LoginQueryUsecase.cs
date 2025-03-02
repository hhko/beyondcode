using DddGym.Framework.BaseTypes.Application.Cqrs;
using ErrorOr;
using GymManagement.Application.Abstractions.Repositories;
using GymManagement.Application.Abstractions.Tokens;
using GymManagement.Application.Usecases.Authentication;
using GymManagement.Application.Usecases.Profiles;
using GymManagement.Domain.AggregateRoots.Users;
using static GymManagement.Application.Usecases.Authentication.Errors.ApplicationErrors;

namespace GymManagement.Application.Usecases.Users.Queries.Login;

internal sealed class LoginQueryUsecase
    : IQueryUsecase<LoginQuery, LoginResponse>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUsersRepository _usersRepository;

    public LoginQueryUsecase(
        IJwtTokenGenerator jwtTokenGenerator, 
        IPasswordHasher passwordHasher, 
        IUsersRepository usersRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordHasher = passwordHasher;
        _usersRepository = usersRepository;
    }

    public async Task<IErrorOr<LoginResponse>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        User? user = await _usersRepository.GetByEmailAsync(query.Email);
        if (user is null)
        {
            return Error.
                NotFound(description: "User not found")
                .ToErrorOr<LoginResponse>();
        }

        if (!user.IsCorrectPasswordHash(query.Password, _passwordHasher))
        {
            return LoginQueryErrors
                .InvalidCredentials
                .ToErrorOr<LoginResponse>();
        }

        return user
            .ToResponse(_jwtTokenGenerator.GenerateToken(user))
            .ToErrorOr();
    }
}