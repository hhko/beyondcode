using DddGym.Framework.BaseTypes.Application.Cqrs;
using GymManagement.Domain.AggregateRoots.Users;
using ErrorOr;
using GymManagement.Application.Abstractions.Repositories;

namespace GymManagement.Application.Usecases.Users.Commands.CreateTrainerProfile;

internal sealed class CreateTrainerProfileCommandUsecase
    : ICommandUsecase<CreateTrainerProfileCommand>
{
    private readonly IUsersRepository _usersRepository;

    public CreateTrainerProfileCommandUsecase(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<IErrorOr> Handle(CreateTrainerProfileCommand command, CancellationToken cancellationToken)
    {
        User? user = await _usersRepository.GetByIdAsync(command.UserId);
        if (user is null)
        {
            return Error
                .NotFound(description: "User not found")
                .ToErrorOr();
        }

        ErrorOr<Guid> createTrainerProfileResult = user.CreateTrainerProfile();

        await _usersRepository.UpdateAsync(user);

        return createTrainerProfileResult;
    }
}