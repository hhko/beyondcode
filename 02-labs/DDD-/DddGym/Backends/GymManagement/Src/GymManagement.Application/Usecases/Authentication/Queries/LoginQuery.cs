using FluentValidation;
using FunctionalDdd.Framework.BaseTypes.Cqrs;
using GymManagement.Domain.AggregateRoots.Users;

namespace GymManagement.Application.Usecases.Authentication.Queries;

public static class LoginQuery
{
    public sealed record Request(
        string Email,
        string Password) : IQuery<Response>;

    public sealed record Response(
        User User,
        string Token)
        : IResponse;

    internal sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Email)
                .NotEmpty();

            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }

    //// TODO: LanguageExt
    //internal sealed class LoginQueryUsecase
    //    : IQueryUsecase<LoginQuery, LoginResponse>
    //{
    //    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    //    private readonly IPasswordHasher _passwordHasher;
    //    private readonly IUsersRepository _usersRepository;

    //    public LoginQueryUsecase(
    //        IJwtTokenGenerator jwtTokenGenerator,
    //        IPasswordHasher passwordHasher,
    //        IUsersRepository usersRepository)
    //    {
    //        _jwtTokenGenerator = jwtTokenGenerator;
    //        _passwordHasher = passwordHasher;
    //        _usersRepository = usersRepository;
    //    }

    //    public async Task<IErrorOr<LoginResponse>> Handle(LoginQuery query, CancellationToken cancellationToken)
    //    {
    //        User? user = await _usersRepository.GetByEmailAsync(query.Email);
    //        if (user is null)
    //        {
    //            return Error.
    //                NotFound(description: "User not found")
    //                .ToErrorOr<LoginResponse>();
    //        }

    //        if (!user.IsCorrectPasswordHash(query.Password, _passwordHasher))
    //        {
    //            return LoginQueryErrors
    //                .InvalidCredentials
    //                .ToErrorOr<LoginResponse>();
    //        }

    //        return user
    //            .ToResponse(_jwtTokenGenerator.GenerateToken(user))
    //            .ToErrorOr();
    //    }
    //}
}

