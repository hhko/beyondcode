﻿using DddGym.Framework.BaseTypes.Events;
using ErrorOr;

namespace GymManagement.Domain.AggregateRoots.Sessions.Errors;

public static partial class DomainEventErrors
{
    public static class SessionCanceledEventErrors
    {
        public static readonly Error TrainerNotFound = DomainEventError.From(
            code: $"{nameof(DomainEventErrors)}.{nameof(Session)}.{nameof(TrainerNotFound)}",
            description: "Trainer not found");

        public static readonly Error TrainerScheduleUpdateFailed = DomainEventError.From(
            code: $"{nameof(DomainEventErrors)}.{nameof(Session)}.{nameof(TrainerScheduleUpdateFailed)}",
            description: "Removing session from trainer's schedule failed");

        public static readonly Error ParticipantScheduleUpdateFailed = DomainEventError.From(
            code: $"{nameof(DomainEventErrors)}.{nameof(Session)}.{nameof(ParticipantScheduleUpdateFailed)}",
            description: "Removing session from participant schedule failed");
    }
}