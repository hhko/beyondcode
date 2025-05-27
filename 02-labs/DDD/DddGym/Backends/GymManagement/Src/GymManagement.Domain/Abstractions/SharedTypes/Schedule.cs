﻿using GymDdd.Framework.BaseTypes;
using GymManagement.Domain.Abstractions.SharedTypes.ValueObjects;
using LanguageExt;
using System.Globalization;
using static GymManagement.Domain.SharedTypes.Errors.DomainErrors;
using static LanguageExt.Prelude;

namespace GymManagement.Domain.Abstractions.SharedTypes;

public sealed partial class Schedule : Entity
{
    private readonly Dictionary<DateOnly, List<TimeSlot>> _calendar = [];

    private Schedule
    (
        Option<Dictionary<DateOnly, List<TimeSlot>>> calendar,
        Option<Guid> id = default
    ) 
        : base(id.IfNone(Guid.NewGuid()))
    {
        _calendar = calendar.IfNone([]);
    }

    private Schedule()
    {
    }

    public static Schedule Create()
    {
        return new Schedule(
            calendar: default,
            id: Guid.NewGuid());
    }

    public static Schedule Empty()
    {
        return Create();
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
               from _2 in ApplyTimeSlotBooking(timeSlots, newTimeSlot)
               select unit;

        // 실패 가능성은 없지만, 내부 상태를 변경하는 부수 효과가 있기 때문에 Fin 모나드를 사용
        Fin<List<TimeSlot>> GetOrCreateTimeSlots(DateOnly date)
        {
            if (!_calendar.TryGetValue(date, out List<TimeSlot>? slots))
            {
                //slots = new List<TimeSlot>();
                slots = [];
                _calendar[date] = slots;
            }

            return slots;
        }

        Fin<Unit> EnsureTimeSlotNotOverlapped(DateOnly date, List<TimeSlot> timeSlots, TimeSlot newTimeSlot) =>
            timeSlots.Any(timeSlot => timeSlot.OverlapsWith(newTimeSlot))
                ? ScheduleErrors.TimeSlotOverlapped(date, newTimeSlot)
                : unit;

        // 실패 가능성은 없지만, 내부 상태를 변경하는 부수 효과가 있기 때문에 Fin 모나드를 사용
        Fin<Unit> ApplyTimeSlotBooking(List<TimeSlot> timeSlots, TimeSlot newTimeSlot)
        {
            timeSlots.Add(newTimeSlot);
            return unit;
        }

        // =========================================
        // Monad 스타일
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

    internal Fin<Unit> UnbookTimeSlot(DateOnly date, TimeSlot timeRange)
    {
        return from _1 in EnsureTimeSlotsAlreadyExit(date, timeRange)
               let timeSlots = GetTimeSlots(date)                     // Map
               from _2 in ApplyTimeSlotCancellation(timeSlots, timeRange)
               select unit;

        //Fin<Unit> EnsureTimeSlotsAlreadyExit(DateOnly date, TimeSlot timeRange) =>
        //    (!_calendar.TryGetValue(date, out var timeSlots) || !timeSlots.Contains(timeRange))
        //        ? ScheduleErrors.TimeSlotNotFound(date, timeRange)
        //        : unit;

        Fin<Unit> EnsureTimeSlotsAlreadyExit(DateOnly date, TimeSlot timeRange) =>
            !_calendar.TryGetValue(date, out var timeSlots)
                ? ScheduleErrors.DateNotFound(date)
                : !timeSlots.Contains(timeRange)
                    ? ScheduleErrors.TimeSlotNotFound(date, timeRange)
                    : unit;

        List<TimeSlot> GetTimeSlots(DateOnly date) =>
            _calendar.GetValueOrDefault(date)!;

        Fin<Unit> ApplyTimeSlotCancellation(List<TimeSlot> timeSlots, TimeSlot timeRange)
        {
            timeSlots.Remove(timeRange);
            return unit;
        }

        // =========================================
        // Monad 스타일
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
}