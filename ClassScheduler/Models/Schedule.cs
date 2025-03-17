/*
Description: Represents the schedule of a course (days of week, time).
Author: Kaeden Peterson 11858249
Date: 3-17-25
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassScheduler.Models;

public class Schedule
{
    public List<DayOfWeek> Days { get; set; }
    public TimeSpan Start { get; set; }
    public TimeSpan End { get; set; }

    public Schedule(List<DayOfWeek> days, TimeSpan start, TimeSpan end)
    {
        Days = days;
        Start = start;
        End = end;
    }

    // For printing in this format: Mon/Wed/Fri 9:00 - 10:00
    public override string ToString()
    {
        return $"{string.Join("/", Days.Select(days => days.ToString()[..3]))} {Start} - {End}";
    }
}