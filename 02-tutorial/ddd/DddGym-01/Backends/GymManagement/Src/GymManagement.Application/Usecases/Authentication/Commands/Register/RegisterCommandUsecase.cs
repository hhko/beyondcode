using DddGym.Framework.BaseTypes.Cqrs;
using GymManagement.Application.Abstractions.TokenGenerator;
using GymManagement.Domain.AggregateRoots.Users;

namespace GymManagement.Application.Usecases.Authentication.Commands.Register;

//// TODO: LanguageExt
//internal sealed class RegisterCommandUsecase
//    : ICommandUsecase<RegisterCommand, RegisterResponse>
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

//    public async Task<IErrorOr<RegisterResponse>> Handle(RegisterCommand command, CancellationToken cancellationToken)
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