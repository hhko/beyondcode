using FluentValidation;
using GymDdd.Framework.BaseTypes.Cqrs;
using GymManagement.Application.Abstractions.TokenGenerator;
using GymManagement.Application.Usecases.Profiles;
using GymManagement.Domain.AggregateRoots.Users;

namespace GymManagement.Application.Usecases.Authentication.Commands;

public static class RegisterCommand
{
    public sealed record Request(
        string FirstName,
        string LastName,
        string Email,
        string Password) : ICommandReqeust<Response>;

    public sealed record Response(
        User User,
        string Token) : IResponse;

    internal sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty();

            RuleFor(x => x.LastName)
                .NotEmpty();

            RuleFor(x => x.Email)
                .NotEmpty();

            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }

    internal sealed class Usecase(
        IJwtTokenGenerator jwtTokenGenerator,
        IPasswordHasher passwordHasher,
        IUsersRepository usersRepository) : ICommandUsecase<Request, Response>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;
        private readonly IUsersRepository _usersRepository = usersRepository;

        public async Task<Fin<Response>> Handle(Request request, CancellationToken cancellationToken)
        {
            var usecase = from _1 in _usersRepository.ExistsByEmailAsync(request.Email)
                          from hashPassword in _passwordHasher.HashPassword(request.Password)
                          let user = User.Create(
                              request.FirstName,
                              request.LastName,
                              request.Email,
                              hashPassword)
                          from _2 in _usersRepository.AddUserAsync(user)
                          from token in _jwtTokenGenerator.GenerateToken(user)
                          select (user, token);

            var result = await usecase
                .Run()
                .RunAsync();

            return result.ToRegisterResponse();
        }
    }
}