﻿using DddGym.Framework.BaseTypes.Cqrs;

namespace GymManagement.Application.Usecases.Gyms.Queries.ListGyms;

public sealed record ListGymsQuery(
    Guid SubscriptionId)
    : IQuery<ListGymsResponse>;