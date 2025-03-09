using DddGym.Framework.BaseTypes.Application.Cqrs;
using ErrorOr;
using GymManagement.Application.Abstractions.Repositories;
using GymManagement.Domain.AggregateRoots.Users;

namespace GymManagement.Application.Usecases.Profiles.Commands.CreateAdminProfile;

internal sealed class CreateAdminProfileCommandUsecase
    : ICommandUsecase<CreateAdminProfileCommand>
{
    private readonly IUsersRepository _usersRepository;

    public CreateAdminProfileCommandUsecase(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<IErrorOr> Handle(CreateAdminProfileCommand command, CancellationToken cancellationToken)
    {
        User? user = await _usersRepository.GetByIdAsync(command.UserId);
        if (user is null)
        {
            return Error
                .NotFound(description: "User not found")
                .ToErrorOr<Guid>();
        }

        ErrorOr<Guid> createAdminProfileResult = user.CreateAddminProfile();
        if (createAdminProfileResult.IsError)
        {
            return createAdminProfileResult
                .Errors
                .ToErrorOr<Guid>();
        }

        await _usersRepository.UpdateAsync(user);

        return createAdminProfileResult;
    }
}