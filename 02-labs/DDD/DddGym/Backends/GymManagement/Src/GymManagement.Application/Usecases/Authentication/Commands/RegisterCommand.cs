using FluentValidation;
using GymDdd.Framework.BaseTypes.Cqrs;
using GymManagement.Domain.AggregateRoots.Users;

namespace GymManagement.Application.Usecases.Authentication.Commands;

public static class RegisterCommand
{
    public sealed record Request(
        string FirstName,
        string LastName,
        string Email,
        string Password)
        : ICommand<Response>; //ICommand<RegisterResponse>;

    public sealed record Response(
        User User,
        string Token)
        : IResponse;

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

    //// TODO: LanguageExt
    //internal sealed class Usecase
    //    : ICommandUsecase<Request, Response>
    //{
    //    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    //    private readonly IPasswordHasher _passwordHasher;
    //    private readonly IUsersRepository _usersRepository;

    //    public RegisterCommandUsecase(
    //        IJwtTokenGenerator jwtTokenGenerator,
    //        IPasswordHasher passwordHasher,
    //        IUsersRepository usersRepository)
    //    {
    //        _jwtTokenGenerator = jwtTokenGenerator;
    //        _passwordHasher = passwordHasher;
    //        _usersRepository = usersRepository;
    //    }

    //    public async Task<IErrorOr<Response>> Handle(Request command, CancellationToken cancellationToken)
    //    {
    //        if (await _usersRepository.ExistsByEmailAsync(command.Email))
    //        {
    //            return Error
    //                .Conflict(description: "User already exists")
    //                .ToErrorOr<RegisterResponse>();
    //        }

    //        ErrorOr<string> hashPasswordResult = _passwordHasher.HashPassword(command.Password);
    //        if (hashPasswordResult.IsError)
    //        {
    //            return hashPasswordResult
    //                .Errors
    //                .ToErrorOr<RegisterResponse>();
    //        }

    //        User user = new User(
    //            command.FirstName,
    //            command.LastName,
    //            command.Email,
    //            hashPasswordResult.Value);

    //        await _usersRepository.AddUserAsync(user);

    //        string token = _jwtTokenGenerator.GenerateToken(user);

    //        return user
    //            .ToResponseRegistered(token)
    //            .ToErrorOr();
    //    }
    //}
}