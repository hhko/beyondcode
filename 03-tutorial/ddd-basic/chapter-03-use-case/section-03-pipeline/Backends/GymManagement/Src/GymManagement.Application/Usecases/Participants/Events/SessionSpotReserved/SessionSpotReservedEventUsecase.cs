using DddGym.Framework.BaseTypes.Application.Events;
using GymManagement.Application.Abstractions.Repositories;
using GymManagement.Domain.AggregateRoots.Sessions.Events;
using Throw;
using static GymManagement.Domain.AggregateRoots.Sessions.Errors.DomainEventErrors;

namespace GymManagement.Application.Usecases.Participants.Events.SessionSpotReserved;

internal sealed class SessionSpotReservedEventUsecase
    : IDomainEventUsecase<SessionSpotReservedEvent>
{
    private readonly IParticipantsRepository _participantsRepository;

    public SessionSpotReservedEventUsecase(IParticipantsRepository participantsRepository)
    {
        _participantsRepository = participantsRepository;
    }

    public async Task Handle(SessionSpotReservedEvent domainEvent, CancellationToken cancellationToken)
    {
        var participant = await _participantsRepository.GetByIdAsync(domainEvent.Reservation.ParticipantId);
        participant.ThrowIfNull();

        var addScheduleResult = participant.AddToSchedule(domainEvent.Session);

        if (addScheduleResult.IsError)
        {
            throw new DomainEventException(
                SessionSpotReservedEventErrors.ParticipantScheduleUpdateFailed,
                addScheduleResult.Errors);
        }

        await _participantsRepository.UpdateAsync(participant);
    }
}
