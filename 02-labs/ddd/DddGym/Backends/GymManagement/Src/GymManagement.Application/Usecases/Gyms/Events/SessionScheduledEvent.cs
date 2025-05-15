using FluentValidation;
using FunctionalDdd.Framework.BaseTypes.Events;
using GymManagement.Domain.AggregateRoots.Gyms;
using Throw;
using static GymManagement.Domain.AggregateRoots.Rooms.Events.DomainEvents;

namespace GymManagement.Application.Usecases.Gyms.Events;

public static class SessionScheduledEvent
{
    internal sealed class Validator
        : AbstractValidator<RoomEvents.SessionScheduledEvent>
    {

    }

    internal sealed class Usecase
        : IDomainEventUsecase<RoomEvents.SessionScheduledEvent>
    {
        private readonly IGymsRepository _gymsRepository;

        public Usecase(IGymsRepository gymsRepository)
        {
            _gymsRepository = gymsRepository;
        }

        public async Task Handle(RoomEvents.SessionScheduledEvent domainEvent, CancellationToken cancellationToken)
        {
            var gym = await _gymsRepository.GetByIdAsync(domainEvent.RoomId);
            gym.ThrowIfNull();

            gym.AddTrainer(domainEvent.Session.TrainerId);
        }
    }
}