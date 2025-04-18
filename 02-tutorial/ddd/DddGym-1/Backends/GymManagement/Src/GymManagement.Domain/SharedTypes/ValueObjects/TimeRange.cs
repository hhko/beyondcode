using DddGym.Framework.BaseTypes;

//
using LanguageExt;
using LanguageExt.Common;
using Throw;

namespace GymManagement.Domain.SharedTypes.ValueObjects;

public sealed class TimeRange : ValueObject
{
    public TimeOnly Start { get; init; }
    public TimeOnly End { get; init; }

    //public TimeRange(TimeOnly start, TimeOnly end)
    //{
    //    Start = start.Throw().IfGreaterThanOrEqualTo(end);
    //    End = end;
    //}

    private TimeRange(TimeOnly start, TimeOnly end)
    {
        Start = start.Throw().IfGreaterThanOrEqualTo(end);
        End = end;
    }

    public static Fin<TimeRange> Create(TimeOnly start, TimeOnly end)
    {
        if (start >= end)
        {
            //return Error.Validation(description: "End time must be greater than the start time.");
            return Error.New(message: "End time must be greater than the start time.");
            //return Errors.ValidationFailed;
        }

        return new TimeRange(
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

    public bool OverlapsWith(TimeRange other)
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