using DddGym.Framework.BaseTypes;
using GymManagement.Domain.SharedTypes.ValueObjects;
using LanguageExt;
using System.Diagnostics.Contracts;
using static GymManagement.Domain.SharedTypes.Errors.DomainErrors;
using static LanguageExt.Prelude;

namespace GymManagement.Domain.SharedTypes;

public sealed partial class Schedule : Entity
{
    private readonly Dictionary<DateOnly, List<TimeSlot>> _calendar = [];

    private Schedule(
        Dictionary<DateOnly, List<TimeSlot>>? calendar = null,
        Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        _calendar = calendar ?? [];
    }

    public static Schedule Empty()
    {
        return new Schedule(id: Guid.NewGuid());
    }

    internal bool CanBookTimeSlot(DateOnly date, TimeSlot timeSlot)
    {
        if (!_calendar.TryGetValue(date, out var timeSlots))
        {
            return true;
        }

        return !timeSlots.Any(timeSlot => timeSlot.OverlapsWith(timeSlot));
    }

    internal Fin<Unit> BookTimeSlot(DateOnly date, TimeSlot newTimeSlot)
    {
        return from timeSlots in GetOrCreateTimeSlots(date)
               from _1 in EnsureTimeSlotNotOverlapped(date, timeSlots, newTimeSlot)
               from _2 in ApplyTimeSlotAddition(timeSlots, newTimeSlot)
               select unit;

        // =========================================
        // Monadic 스타일
        // =========================================

        //return GetOrCreateTimeSlots(date)
        //    .Bind(timeSlots => CheckOverlappingTimeSlot(date, timeSlots, newTimeSlot))      // Unit -> List<TimeSlot> 변경 필요
        //    .Bind(timeSlots => ApplyTimeSlotToCalendar(timeSlots, newTimeSlot);

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
    private Fin<List<TimeSlot>> GetOrCreateTimeSlots(DateOnly date)
    {
        if (!_calendar.TryGetValue(date, out List<TimeSlot>? slots))
        {
            //slots = new List<TimeSlot>();
            slots = [];
            _calendar[date] = slots;
        }

        return slots;
    }

    [Pure]
    private Fin<Unit> EnsureTimeSlotNotOverlapped(DateOnly date, List<TimeSlot> timeSlots, TimeSlot newTimeSlot) =>
        timeSlots.Any(timeSlot => timeSlot.OverlapsWith(newTimeSlot))
            ? ScheduleErrors.TimeSlotOverlapped(date, newTimeSlot)
            : unit;

    // 실패 가능성은 없지만, 내부 상태를 변경하는 부수 효과가 있기 때문에 Fin 모나드를 사용
    private static Fin<Unit> ApplyTimeSlotAddition(List<TimeSlot> timeSlots, TimeSlot newTimeSlot)
    {
        timeSlots.Add(newTimeSlot);
        return unit;
    }

    internal Fin<Unit> UnbookTimeSlot(DateOnly date, TimeSlot timeRange)
    {
        return from _1 in EnsureTimeSlotsAlreadyExit(date, timeRange)
               from timeSlots in Pure(GetTimeSlots(date))                     // Map
               from _2 in ApplyTimeSlotRemoval(timeSlots, timeRange)
               select unit;

        // =========================================
        // Monadic 스타일
        // =========================================

        //return EnsureTimeSlotsAlreadyExit(date, timeRange)
        //    .Map(_ => GetTimeSlots(date))
        //    .Bind(timeSlots => ApplyTimeSlotRemoval(timeSlots, timeRange));

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

    private Fin<Unit> EnsureTimeSlotsAlreadyExit(DateOnly date, TimeSlot timeRange) =>
        (!_calendar.TryGetValue(date, out var timeSlots) || !timeSlots.Contains(timeRange))
            ? ScheduleErrors.TimeSlotNotFound(date, timeRange)
            : unit;


    private List<TimeSlot> GetTimeSlots(DateOnly date) =>
        _calendar.GetValueOrDefault(date)!;

    private static Fin<Unit> ApplyTimeSlotRemoval(List<TimeSlot> timeSlots, TimeSlot timeRange)
    {
        timeSlots.Remove(timeRange);
        return unit;
    }
}