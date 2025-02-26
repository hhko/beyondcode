using DddGym.Framework.BaseTypes.Domain;

namespace DddGym.Framework.IntegrationEvents;

public sealed record SessionScheduledIntegrationEvent(
    Guid RoomId,
    Guid TrainerId)
    : IIntegrationEvent;