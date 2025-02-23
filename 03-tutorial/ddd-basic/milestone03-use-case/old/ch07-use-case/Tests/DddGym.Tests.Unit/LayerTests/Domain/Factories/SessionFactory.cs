using DddGym.Domain.Abstractions.ValueObjects;
using DddGym.Domain.AggregateRoots.Sessions;
using DddGym.Domain.AggregateRoots.Sessions.Enumerations;
using DddGym.Tests.Unit.LayerTests.Domain.Constants;

namespace DddGym.Tests.Unit.LayerTests.Domain.Factories;

internal static class SessionFactory
{
    public static Session CreateSession(
        string name = DomainConstants.Session.Name,
        string description = DomainConstants.Session.Description,
        Guid? roomId = null,
        Guid? trainerId = null,
        int maxParticipants = DomainConstants.Session.MaxParticipants,
        DateOnly? date = null,
        TimeRange? time = null,
        List<SessionCategory> categories = null,
        Guid? id = null)
    {
        return new Session(
            name: DomainConstants.Session.Name,
            description: DomainConstants.Session.Description,
            maxParticipants: maxParticipants,
            roomId: roomId ?? DomainConstants.Room.Id,
            trainerId: trainerId ?? DomainConstants.Trainer.Id,
            date ?? DomainConstants.Session.Date,
            time ?? DomainConstants.Session.Time,
            categories: categories ?? DomainConstants.Session.Categories,
            id: id ?? DomainConstants.Session.Id);
    }
}