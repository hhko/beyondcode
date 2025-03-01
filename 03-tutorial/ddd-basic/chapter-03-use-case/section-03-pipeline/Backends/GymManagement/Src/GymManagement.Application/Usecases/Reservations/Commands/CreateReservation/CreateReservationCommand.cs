using DddGym.Framework.BaseTypes.Application.Cqrs;

namespace GymManagement.Application.Usecases.Sessions.Commands.CreateReservation;

public sealed record CreateReservationCommand(
    Guid SessionId,
    Guid ParticipantId)
    : ICommand;