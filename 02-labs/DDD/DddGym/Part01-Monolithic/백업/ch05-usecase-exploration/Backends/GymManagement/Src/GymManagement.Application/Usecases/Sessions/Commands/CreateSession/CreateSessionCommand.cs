using DddGym.Framework.BaseTypes.Cqrs;
using GymManagement.Domain.AggregateRoots.Sessions.Enumerations;

namespace GymManagement.Application.Usecases.Sessions.Commands.CreateSession;

public sealed record CreateSessionCommand(
    Guid RoomId,
    string Name,
    string Description,
    int MaxParticipants,
    DateTime StartDateTime,
    DateTime EndDateTime,
    Guid TrainerId,
    List<SessionCategory> Categories) : ICommand<CreateSessionResponse>;