using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassScheduler.Models;

/// <summary>
/// Represents the schedule of a course (days of week, class time, start/end date).
/// Includes formatted representations of the schedule for display purposes.
/// </summary>
public class Schedule(
    List<DayOfWeek> days,
    TimeSpan start,
    TimeSpan end,
    DateTime startDate,
    DateTime endDate)
{
    public List<DayOfWeek> Days { get; } = days;
    public TimeSpan StartTime { get; } = start;
    public TimeSpan EndTime { get; } = end;
    public DateTime StartDate { get; } = startDate;
    public DateTime EndDate { get; } = endDate;

    public string FormattedStartDate => StartDate.ToString("MM/dd/yyyy");
    public string FormattedEndDate => EndDate.ToString("MM/dd/yyyy");

    // Format: Mon/Wed/Fri 9:00 - 10:00
    public override string ToString() => 
        $"{string.Join("/", Days.Select(days => days.ToString()[..3]))} " +
        $"{FormatTime(StartTime)} - {FormatTime(EndTime)}";
    
    // Formats from 24 hour to 12 hour time and adds either AM or PM
    public string FormatTime(TimeSpan time)
    {
        DateTime dt = DateTime.Today.Add(time);
        return dt.ToString("h:mmtt");
    }
}