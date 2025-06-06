﻿using DddGym.Domain.AggregateRoots.Sessions;

namespace DddGym.Tests.Unit.Abstractions.Providers;

public sealed class TestDateTimeProvider : IDateTimeProvider
{
    private readonly DateTime? _fixedDateTime;

    public DateTime UtcNow => _fixedDateTime ?? DateTime.UtcNow;

    public TestDateTimeProvider(DateTime? fixedDateTime = null)
    {
        _fixedDateTime = fixedDateTime;
    }
}