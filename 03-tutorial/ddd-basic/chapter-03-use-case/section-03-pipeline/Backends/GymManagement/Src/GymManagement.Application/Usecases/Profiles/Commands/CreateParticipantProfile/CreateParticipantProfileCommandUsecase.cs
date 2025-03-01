using DddGym.Framework.BaseTypes.Application.Cqrs;
using GymManagement.Domain.AggregateRoots.Users;
using ErrorOr;
using GymManagement.Application.Abstractions.Repositories;

namespace GymManagement.Application.Usecases.Users.Commands.CreateParticipantProfile;

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