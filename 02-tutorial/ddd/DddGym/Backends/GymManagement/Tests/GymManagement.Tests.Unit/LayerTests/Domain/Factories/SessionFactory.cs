using GymManagement.Domain.AggregateRoots.Sessions;
using GymManagement.Domain.AggregateRoots.Sessions.Enumerations;
using GymManagement.Domain.SharedTypes.ValueObjects;
using GymManagement.Tests.Unit.LayerTests.Domain.Constants;

namespace GymManagement.Tests.Unit.LayerTests.Domain.Factories;

internal static class SessionFactory
{
    public static Session CreateSession(
        string name = DomainConstants.Session.Name,
        string description = DomainConstants.Session.Description,
        Guid? roomId = null,
        Guid? trainerId = null,
        int maxParticipants = DomainConstants.Session.MaxParticipants,
        DateOnly? date = null,
        TimeSlot? timeSlot = null,
        List<SessionCategory>? categories = null,
        Guid? id = null)
    {
        return Session.Create(
            name: name,
            description: description,
            maxParticipants: maxParticipants,
            roomId: roomId ?? DomainConstants.Room.Id,
            trainerId: trainerId ?? DomainConstants.Trainer.Id,
            date: date ?? DomainConstants.Session.Date,
            timeSlot: timeSlot ?? DomainConstants.Session.TimeSlot,
            categories: categories ?? DomainConstants.Session.Categories,
            id: id ?? DomainConstants.Session.Id);
    }
}