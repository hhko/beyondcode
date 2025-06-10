﻿using DddGym.Framework.BaseTypes.Cqrs;

namespace GymManagement.Application.Usecases.Reservations.Commands.CreateReservation;

public sealed record CreateReservationCommand(
    Guid SessionId,
    Guid ParticipantId)
    : ICommand;