using DddGym.Framework.BaseTypes;
using GymManagement.Domain.SharedTypes.ValueObjects;
using LanguageExt;
using System.Diagnostics.Contracts;
using static GymManagement.Domain.SharedTypes.Errors.DomainErrors;
using static LanguageExt.Prelude;

namespace GymManagement.Domain.SharedTypes;

public sealed class Schedule : Entity
{
    private readonly Dictionary<DateOnly, List<TimeRange>> _calendar = [];

    private Schedule(
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

    //internal Fin<Unit> BookTimeSlot(DateOnly date, TimeRange time)
    internal Fin<Unit> BookTimeSlot(DateOnly date, TimeRange newTimeSlot)
    {
        return from timeSlots in GetOrCreateTimeSlots(date)
               from _1 in CheckOverlap(date, timeSlots, newTimeSlot)
               from _2 in ApplyTimeSlotToCalendar(timeSlots, newTimeSlot)
               select unit;

        // =========================================
        // Imperative Guard 스타일
        // =========================================

        //if (!_calendar.TryGetValue(date, out var timeSlots))
        //{
        //    _calendar[date] = [time];
        //    return unit;
        //}
        //
        //if (timeSlots.Any(timeSlot => timeSlot.OverlapsWith(time)))
        //{
        //    return ScheduleErrors.CannotHaveTwoOrMoreOverlappingSessions(date, time);
        //}
        //
        //timeSlots.Add(time);
        //return unit;
    }

    // 실패 가능성은 없지만, 내부 상태를 변경하는 부수 효과가 있기 때문에 Fin 모나드를 사용
    private Fin<List<TimeRange>> GetOrCreateTimeSlots(DateOnly date)
    {
        if (!_calendar.TryGetValue(date, out var slots))
        {
            slots = new List<TimeRange>();
            _calendar[date] = slots;
        }

        return slots;
    }

    private Fin<Unit> CheckOverlap(DateOnly date, List<TimeRange> timeSlots, TimeRange newTimeSlot) =>
        timeSlots.Any(timeSlot => timeSlot.OverlapsWith(newTimeSlot))
            ? ScheduleErrors.CannotHaveTwoOrMoreOverlappingTimeSlot(date, newTimeSlot)
            : unit;

    // 실패 가능성은 없지만, 내부 상태를 변경하는 부수 효과가 있기 때문에 Fin 모나드를 사용
    private Fin<Unit> ApplyTimeSlotToCalendar(List<TimeRange> timeSlots, TimeRange newTimeSlot)
    {
        timeSlots.Add(newTimeSlot);
        return unit;
    }

    internal Fin<Unit> UnbookTimeSlot(DateOnly date, TimeRange timeRange)
    {
        return from timeSlots in GetTimeSlots(date, timeRange)
               from _ in RemoveFromCalendar(timeSlots, timeRange)
               select unit;

        // =========================================
        // Imperative Guard 스타일
        // =========================================

        //if (!_calendar.TryGetValue(date, out var timeSlots) || !timeSlots.Contains(time))
        //{
        //    return Error.New("Booking not found");
        //}
        //
        //timeSlots.Remove(time);
        //return unit;
        //
        //return unit;
    }

    private Fin<List<TimeRange>> GetTimeSlots(DateOnly date, TimeRange timeRange) =>
        (!_calendar.TryGetValue(date, out var timeSlots) || !timeSlots.Contains(timeRange))
            ? ScheduleErrors.CannotFindTheTimeSlot(date, timeRange)
            : timeSlots;

    [Pure]
    private Fin<Unit> RemoveFromCalendar(List<TimeRange> timeSlots, TimeRange timeRange)
    {
        timeSlots.Remove(timeRange);
        return unit;
    }
}