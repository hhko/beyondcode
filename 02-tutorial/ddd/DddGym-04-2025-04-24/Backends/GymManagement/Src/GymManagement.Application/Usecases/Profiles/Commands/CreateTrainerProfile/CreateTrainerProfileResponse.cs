using DddGym.Framework.BaseTypes.Cqrs;
using LanguageExt;

namespace GymManagement.Application.Usecases.Profiles.Commands.CreateTrainerProfile;

public sealed record CreateTrainerProfileResponse(
    Option<Guid> TrainerId = default)
    : IResponse;