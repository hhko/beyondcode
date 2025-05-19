using FluentValidation;
using GymDdd.Framework.BaseTypes.Cqrs;
using GymManagement.Domain.AggregateRoots.Users;
using LanguageExt;
using LanguageExt.Common;

namespace GymManagement.Application.Usecases.Profiles.Commands;

public static class CreateTrainerProfileCommand
{
    public sealed record Request(
        Guid UserId)
        : ICommand<Response>;

    public sealed record Response(
        Option<Guid> TrainerId = default)
        : IResponse;

    internal sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(_ => _.UserId)
                .NotEmpty();
        }
    }

    internal sealed class Usecase(
        IUsersRepository usersRepository)
        : ICommandUsecase<Request, Response>
    {
        private readonly IUsersRepository _usersRepository = usersRepository;

        public async Task<Fin<Response>> Handle(Request request, CancellationToken cancellationToken)
        {
            //return from user in await _usersRepository.GetByIdAsync(request.UserId)
            //       from trainerId in user.CreateTrainerProfile()
            //       //from _ in Pure(await _usersRepository.UpdateAsync(user))
            //       select new CreateTrainerProfileResponse(trainerId);

            //(await _usersRepository.GetByIdAsync(request.UserId))
            //    .Bind(user => user.CreateTrainerProfile())

            Fin<User> userResult = await _usersRepository.GetByIdAsync(request.UserId);
            if (userResult.IsFail)
            {
                return (Error)userResult;
            }
            User user = (User)userResult;

            Fin<Guid> trainerResult = user.CreateTrainerProfile();
            if (trainerResult.IsFail)
            {
                return (Error)trainerResult;
            }
            Guid trainerId = (Guid)trainerResult;

            await _usersRepository.UpdateAsync(user);

            return new Response(trainerId);
        }

        //public async Task<IErrorOr> Handle(CreateTrainerProfileCommand command, CancellationToken cancellationToken)
        //{
        //    User? user = await _usersRepository.GetByIdAsync(command.UserId);
        //    if (user is null)
        //    {
        //        return Error
        //            .NotFound(description: "User not found")
        //            .ToErrorOr();
        //    }

        //    ErrorOr<Guid> createTrainerProfileResult = user.CreateTrainerProfile();

        //    await _usersRepository.UpdateAsync(user);

        //    return createTrainerProfileResult;
        //}

        // 원본 코드
        //public async Task<ErrorOr<Guid>> Handle(CreateAdminProfileCommand command, CancellationToken cancellationToken)
        //{
        //    var user = await _usersRepository.GetByIdAsync(command.UserId);
        //
        //    if (user is null)
        //    {
        //        return Error.NotFound(description: "User not found");
        //    }
        //
        //    var createAdminProfileResult = user.CreateAdminProfile();
        //
        //    await _usersRepository.UpdateAsync(user);
        //
        //    return createAdminProfileResult;
        //}
    }
}