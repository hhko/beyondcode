using DddGym.Framework.BaseTypes.Cqrs;
using GymManagement.Domain.AggregateRoots.Users;
using LanguageExt;
using LanguageExt.Common;

namespace GymManagement.Application.Usecases.Profiles.Commands.CreateParticipantProfile;

internal sealed class CreateParticipantProfileCommandUsecase(
    IUsersRepository usersRepository)
    : ICommandUsecase2<CreateParticipantProfileCommand, CreateParticipantProfileResponse>
{
    private readonly IUsersRepository _usersRepository = usersRepository;

    public async Task<Fin<CreateParticipantProfileResponse>> Handle(CreateParticipantProfileCommand request, CancellationToken cancellationToken)
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

        return new CreateParticipantProfileResponse(participantId);
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