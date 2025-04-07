using ClassScheduler.CoreUI;
using ClassScheduler.Data;
using ClassScheduler.ViewModels;
using ClassScheduler.Views;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Linq;

namespace ClassScheduler.Models;

/// <summary>
/// Represents a course in the system. Stores course details.
/// </summary>
public class Course(
    string code,
    string name,
    string instructor,
    string description,
    int credits,
    List<Course> prerequisites,
    int maxSeats,
    string location,
    bool isActive,
    Schedule schedule,
    List<Student> enrolledStudents)
{
    public string Code { get; set; } = code;
    public string Name { get; set; } = name;
    public string Instructor { get; set; } = instructor;
    public string Description { get; set; } = description;
    public int Credits { get; set; } = credits;
    public List<Course> Prerequisites { get; set; } = prerequisites;
    public int MaxSeats { get; set; } = maxSeats;

    public string Location {get; set;} = location;

    public bool IsActive { get; set; } = isActive;
    public Schedule Schedule { get; set; } = schedule;
    
    public List<Student> EnrolledStudents { get; set; } = enrolledStudents;
    
    public int OpenSeats => MaxSeats - EnrolledStudents.Count;
    
    public string Status => IsActive ? "Active" : "Inactive";
    
    public string FormattedPrereqs => 
        Prerequisites.Count > 0 ? string.Join(", ", Prerequisites.Select(p => p.Code)) : "None";
}