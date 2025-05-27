using GymManagement.Domain.Abstractions.SharedTypes.ValueObjects;
using GymManagement.Domain.AggregateRoots.Sessions;
using GymManagement.Domain.AggregateRoots.Sessions.Enumerations;
using GymManagement.Tests.Unit.LayerTests.Domain.Constants;

namespace GymManagement.Tests.Unit.LayerTests.Domain.Factories;

internal static class SessionFactory
{
    public static Session CreateSession(
        string name = DomainConstants.Session.Name,
        string description = DomainConstants.Session.Description,
        Option<Guid> roomId = default,
        Option<Guid> trainerId = default,
        int maxParticipants = DomainConstants.Session.MaxParticipants,
        Option<DateOnly> date = default,
        Option<TimeSlot> timeSlot = default,
        Option<List<SessionCategory>> categories = default,
        Option<Guid> id = default)
    {
        return Session.Create(
            name: name,
            description: description,
            maxParticipants: maxParticipants,
            roomId: roomId.IfNone(DomainConstants.Room.Id),
            trainerId: trainerId.IfNone(DomainConstants.Trainer.Id),
            date: date.IfNone(DomainConstants.Session.Date),
            timeSlot: timeSlot.IfNone(DomainConstants.Session.TimeSlot),
            categories: categories.IfNone(DomainConstants.Session.Categories),
            id: id.IfNone(DomainConstants.Session.Id));
    }
}
