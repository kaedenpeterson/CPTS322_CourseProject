/*
Description: Represents the schedule of a course (days of week, time).
Author: Kaeden Peterson 11858249
Date: 3-17-25
*/

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
    private List<DayOfWeek> Days { get; } = days;
    private TimeSpan StartTime { get; } = start;
    private TimeSpan EndTime { get; } = end;
    private DateTime StartDate { get; } = startDate;
    private DateTime EndDate { get; } = endDate;

    public string FormattedStartDate => StartDate.ToString("MM/dd/yyyy");
    public string FormattedEndDate => EndDate.ToString("MM/dd/yyyy");

    // Format: Mon/Wed/Fri 9:00 - 10:00
    public override string ToString() => 
        $"{string.Join("/", Days.Select(days => days.ToString()[..3]))} " +
        $"{FormatTime(StartTime)} - {FormatTime(EndTime)}";
    
    // Formats from 24 hour to 12 hour time and adds either AM or PM
    private static string FormatTime(TimeSpan time)
    {
        DateTime dt = DateTime.Today.Add(time);
        return dt.ToString("h:mmtt");
    }
}