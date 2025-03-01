using DddGym.Framework.BaseTypes.Application.Cqrs;

namespace GymManagement.Application.Usecases.Participants.Commands.CancelReservation;

public sealed record CancelReservationCommand(
    Guid ParticipantId, 
    Guid SessionId)
    : ICommand;