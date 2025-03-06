using DddGym.Framework.BaseTypes.Application.Cqrs;
using ErrorOr;
using GymManagement.Application.Abstractions.Repositories;
using GymManagement.Domain.AggregateRoots.Participants;
using GymManagement.Domain.AggregateRoots.Sessions;

namespace GymManagement.Application.Usecases.Sessions.Commands.CreateReservation;

internal sealed class CreateReservationCommandUsecase
    : ICommandUsecase<CreateReservationCommand>
{
    private readonly ISessionsRepository _sessionsRepository;
    private readonly IParticipantsRepository _participantsRepository;

    public CreateReservationCommandUsecase(
        ISessionsRepository sessionsRepository,
        IParticipantsRepository participantsRepository)
    {
        _sessionsRepository = sessionsRepository;
        _participantsRepository = participantsRepository;
    }

    public async Task<IErrorOr> Handle(CreateReservationCommand command, CancellationToken cancellationToken)
    {
        Session? session = await _sessionsRepository.GetByIdAsync(command.SessionId);
        if (session is null)
        {
            return Error
                .NotFound(description: "Session not found")
                .ToErrorOr();
        }

        if (session.HasReservationForParticipant(command.ParticipantId))
        {
            return Error
                .Conflict(description: "Participant already has reservation")
                .ToErrorOr();
        }

        Participant? participant = await _participantsRepository.GetByIdAsync(command.ParticipantId);
        if (participant is null)
        {
            return Error
                .NotFound(description: "Participant not found")
                .ToErrorOr();
        }

        if (participant.HasReservationForSession(session.Id))
        {
            return Error
                .Conflict(description: "Participant not expected to have reservation to session")
                .ToErrorOr();
        }

        if (!participant.IsTimeShotFree(session.Date, session.Time))
        {
            return Error
                .Conflict(description: "Participant's calendar is not free for the entire session duration")
                .ToErrorOr();
        }

        ErrorOr<Success> reserveSpotResult = session.ReserveSpot(participant);

        if (reserveSpotResult.IsError)
        {
            return reserveSpotResult
                .Errors
                .ToErrorOr();
        }

        await _sessionsRepository.UpdateAsync(session);

        return Result
            .Success
            .ToErrorOr();
    }
}
