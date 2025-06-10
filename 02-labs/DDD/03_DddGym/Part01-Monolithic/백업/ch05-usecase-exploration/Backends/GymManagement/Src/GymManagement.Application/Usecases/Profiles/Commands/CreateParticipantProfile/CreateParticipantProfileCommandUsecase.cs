using DddGym.Framework.BaseTypes.Cqrs;
using ErrorOr;
using GymManagement.Application.Abstractions.Repositories;
using GymManagement.Domain.AggregateRoots.Users;

namespace GymManagement.Application.Usecases.Profiles.Commands.CreateParticipantProfile;

internal sealed class CreateParticipantProfileCommandUsecase
    : ICommandUsecase<CreateParticipantProfileCommand>
{
    private readonly IUsersRepository _usersRepository;

    public CreateParticipantProfileCommandUsecase(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<IErrorOr> Handle(CreateParticipantProfileCommand command, CancellationToken cancellationToken)
    {
        User? user = await _usersRepository.GetByIdAsync(command.UserId);
        if (user is null)
        {
            return Error
                .NotFound(description: "User not found")
                .ToErrorOr();
        }

        ErrorOr<Guid> createParticipantProfileResult = user.CreateParticipantProfile();

        await _usersRepository.UpdateAsync(user);

        return createParticipantProfileResult;
    }
}