﻿using DddGym.Framework.BaseTypes.Cqrs;
using GymManagement.Domain.AggregateRoots.Sessions;

namespace GymManagement.Application.Usecases.Participants.Queries.ListParticipantSessions;

public sealed record ListParticipantSessionsResponse(
    List<Session> Sessions)
    : IResponse;