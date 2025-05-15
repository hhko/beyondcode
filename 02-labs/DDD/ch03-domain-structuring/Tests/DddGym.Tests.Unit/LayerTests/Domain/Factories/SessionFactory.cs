using DddGym.Domain.Abstractions.ValueObjects;
using DddGym.Domain.AggregateRoots.Sessions;
using DddGym.Tests.Unit.LayerTests.Domain.Constants;

namespace DddGym.Tests.Unit.LayerTests.Domain.Factories;

public static class SessionFactory
{
    public static Session CreateSession(
        DateOnly? date = null,
        TimeRange? time = null,
        int maxParticipants = DomainConstants.Session.MaxParticipants,
        Guid? id = null)
    {
        return new Session(
            date ?? DomainConstants.Session.Date,
            time ?? DomainConstants.Session.Time,
            maxParticipants,
            trainerId: DomainConstants.Trainer.Id,
            id: id ?? DomainConstants.Session.Id);
    }
}