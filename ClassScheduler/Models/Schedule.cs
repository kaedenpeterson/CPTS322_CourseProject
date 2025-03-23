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
    internal List<DayOfWeek> Days { get; set; }
    internal TimeSpan StartTime { get; set; }
    internal TimeSpan EndTime { get; set; }
    
    internal DateTime StartDate { get; set; }
    
    internal DateTime EndDate { get; set; }
    
    public string FormattedStartDate => StartDate.ToString("MM/dd/yyyy");
    public string FormattedEndDate => EndDate.ToString("MM/dd/yyyy");

    public Schedule(List<DayOfWeek> days, TimeSpan start, TimeSpan end,
        DateTime startDate, DateTime endDate)
    {
        Days = days;
        StartTime = start;
        EndTime = end;
        StartDate = startDate;
        EndDate = endDate;
    }

    // Format: Mon/Wed/Fri 9:00 - 10:00
    public override string ToString()
    {
        return $"{string.Join("/", Days.Select(days => days.ToString()[..3]))} " +
               $@"{FormatTime(StartTime)} - {FormatTime(EndTime)}";
    }
    
    // Formats from 24 hour to 12 hour time and adds either AM or PM
    private string FormatTime(TimeSpan time)
    {
        DateTime dt = DateTime.Today.Add(time);
        return dt.ToString("h:mmtt");
    }
}