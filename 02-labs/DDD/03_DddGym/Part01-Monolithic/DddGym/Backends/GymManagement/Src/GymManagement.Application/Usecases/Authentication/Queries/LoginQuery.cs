using FluentValidation;
using GymDdd.Framework.BaseTypes.Cqrs;
using GymManagement.Application.Abstractions.TokenGenerator;
using GymManagement.Application.Usecases.Profiles;
using GymManagement.Domain.AggregateRoots.Users;

namespace GymManagement.Application.Usecases.Authentication.Queries;

public static class LoginQuery
{
    public sealed record Request(
        string Email,
        string Password) : IQueryRequest<Response>;

    public sealed record Response(
        User User,
        string Token) : IResponse;

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

    internal sealed class Usecase(
        IJwtTokenGenerator jwtTokenGenerator,
        IPasswordHasher passwordHasher,
        IUsersRepository usersRepository) : IQueryUsecase<Request, Response>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;
        private readonly IUsersRepository _usersRepository = usersRepository;

        public async Task<Fin<Response>> Handle(Request request, CancellationToken cancellationToken)
        {
            var usecase = from user in _usersRepository.GetByEmailAsync(request.Email)
                          from _ in _passwordHasher.IsCorrectPassword(request.Password, user.PasswordHash)
                          from token in _jwtTokenGenerator.GenerateToken(user)
                          select (user, token);

            var result = await usecase
                .Run()
                .RunAsync();

            return result.ToLoginResponse();
        }
    }
}

