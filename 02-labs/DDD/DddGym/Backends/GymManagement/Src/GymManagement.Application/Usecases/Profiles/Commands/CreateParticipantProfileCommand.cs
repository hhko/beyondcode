using FluentValidation;
using FunctionalDdd.Framework.BaseTypes.Cqrs;
using GymManagement.Domain.AggregateRoots.Users;
using LanguageExt;
using LanguageExt.Common;

namespace GymManagement.Application.Usecases.Profiles.Commands;

public static class CreateParticipantProfileCommand
{
    public sealed record Request(
        Guid UserId)
        : ICommand<Response>;

    public sealed record Response(
        Option<Guid> ParticipantId = default)
        : IResponse;

    internal sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
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
            //       from participantId in user.CreateParticipantProfile()
            //       select new CreateParticipantProfileResponse(participantId);

            Fin<User> userResult = await _usersRepository.GetByIdAsync(request.UserId);
            if (userResult.IsFail)
            {
                return (Error)userResult;
            }
            User user = (User)userResult;

            Fin<Guid> participantResult = user.CreateParticipantProfile();
            if (participantResult.IsFail)
            {
                return (Error)participantResult;
            }
            Guid participantId = (Guid)participantResult;

            await _usersRepository.UpdateAsync(user);

            return new Response(participantId);
        }

        //public async Task<IErrorOr> Handle(CreateParticipantProfileCommand command, CancellationToken cancellationToken)
        //{
        //    User? user = await _usersRepository.GetByIdAsync(command.UserId);
        //    if (user is null)
        //    {
        //        return Error
        //            .NotFound(description: "User not found")
        //            .ToErrorOr();
        //    }

        //    ErrorOr<Guid> createParticipantProfileResult = user.CreateParticipantProfile();

        //    await _usersRepository.UpdateAsync(user);

        //    return createParticipantProfileResult;
        //}
    }
}