using DddGym.Framework.BaseTypes;

//
using LanguageExt;
using LanguageExt.Common;
using Throw;
using static GymManagement.Domain.SharedTypes.Errors.DomainErrors;

namespace GymManagement.Domain.SharedTypes.ValueObjects;

public sealed class TimeSlot : ValueObject
{
    public TimeOnly Start { get; init; }
    public TimeOnly End { get; init; }

    //public TimeRange(TimeOnly start, TimeOnly end)
    //{
    //    Start = start.Throw().IfGreaterThanOrEqualTo(end);
    //    End = end;
    //}

    private TimeSlot(TimeOnly start, TimeOnly end)
    {
        Start = start.Throw().IfGreaterThanOrEqualTo(end);
        End = end;
    }

    public static Fin<TimeSlot> Create(TimeOnly start, TimeOnly end)
    {
        Error error = Error.Empty
            .If(start >= end, TimeSlotErrors.InvalidTimeRange(start, end));

        return error.CreateValueObject(() => new TimeSlot(start, end));
        
        //if (start >= end)
        //{
        //    //return Error.Validation(description: "End time must be greater than the start time.");
        //    return Error.New(message: "End time must be greater than the start time.");
        //    //return Errors.ValidationFailed;
        //}

        return new TimeSlot(
            start: start,
            end: end);
    }

    //public static Fin<TimeRange> FromDateTimes(DateTime start, DateTime end)
    //{
    //    //if (start.Date != end.Date || start >= end)
    //    //{
    //    //    return Error.Validation();
    //    //}

    //    //return new TimeRange(TimeOnly.FromDateTime(start), TimeOnly.FromDateTime(end));

    //    if (start.Date != end.Date)
    //    {
    //        return Error.Validation(description: "Start and end date times must be on the same day.");
    //    }

    //    if (start >= end)
    //    {
    //        return Error.Validation(description: "End time must be greater than the start time.");
    //    }

    //    return new TimeRange(
    //        start: TimeOnly.FromDateTime(start),
    //        end: TimeOnly.FromDateTime(end));
    //}

    public bool OverlapsWith(TimeSlot other)
    {
        if (Start >= other.End)
        {
            return false;
        }

        if (other.Start >= End)
        {
            return false;
        }

        return true;
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Start;
        yield return End;
    }
}