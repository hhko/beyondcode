using DddGym.Framework.BaseTypes.Application.Cqrs;

namespace GymManagement.Application.Usecases.Users.Commands.CreateAdminProfile;

public sealed record CreateAdminProfileCommand(
    Guid UserId)
    : ICommand;