using DddGym.Framework.BaseTypes;
//using ErrorOr;
using GymManagement.Domain.Abstractions.ValueObjects;
using LanguageExt;
using LanguageExt.Common;

namespace GymManagement.Domain.Abstractions.Entities;

public sealed class Schedule : Entity
{
    private readonly Dictionary<DateOnly, List<TimeRange>> _calendar = [];

    public Schedule(
        Dictionary<DateOnly, List<TimeRange>>? calendar = null,
        Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        _calendar = calendar ?? [];
    }

    public static Schedule Empty()
    {
        return new Schedule(id: Guid.NewGuid());
    }

    internal bool CanBookTimeSlot(DateOnly date, TimeRange time)
    {
        if (!_calendar.TryGetValue(date, out var timeSlots))
        {
            return true;
        }

        return !timeSlots.Any(timeSlot => timeSlot.OverlapsWith(time));
    }

    //internal ErrorOr<Success> BookTimeSlot(DateOnly date, TimeRange time)
    internal Fin<Unit> BookTimeSlot(DateOnly date, TimeRange time)
    {
        if (!_calendar.TryGetValue(date, out var timeSlots))
        {
            _calendar[date] = [time];
            //return Result.Success;
            return Unit.Default;
        }

        if (timeSlots.Any(timeSlot => timeSlot.OverlapsWith(time)))
        {
            //return Error.Conflict();
            return Error.New("Conflict");
        }

        timeSlots.Add(time);
        //return Result.Success;
        return Unit.Default;
    }

    //internal ErrorOr<Success> RemoveBooking(DateOnly date, TimeRange time)
    internal Fin<Unit> RemoveBooking(DateOnly date, TimeRange time)
    {
        if (!_calendar.TryGetValue(date, out var timeSlots) || !timeSlots.Contains(time))
        {
            //return Error.NotFound(description: "Booking not found");
            return Error.New("Booking not found");
        }

        if (!timeSlots.Remove(time))
        {
            //return Error.Unexpected();
            return Error.New("Unexpected");
        }

        //return Result.Success;
        return Unit.Default;
    }
}