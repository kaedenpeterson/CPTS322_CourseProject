using System.Collections.Generic;
using System.Linq;
using ClassScheduler.Data;
using System.Windows.Input;

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
    List<string> prerequisites,
    int maxSeats,
    string location,
    bool isActive,
    Schedule schedule)
{
    public string Code { get; set; } = code;
    public string Name { get; set; } = name;
    public string Instructor { get; set; } = instructor;
    public string Description { get; set; } = description;
    public int Credits { get; set; } = credits;
    public List<string> Prerequisites { get; set; } = prerequisites;
    public int MaxSeats { get; set; } = maxSeats;

    public string Location {get; set;} = location;

    public bool IsActive { get; set; } = isActive;
    public Schedule Schedule { get; set; } = schedule;
    public int OpenSeats => MaxSeats - SystemManager.Students.Count(s => s.Courses.Contains(this));
    public ICommand AddToCartCommand { get; set; }
    public bool IsInCart { get; set; }
    
    public string Status => IsActive ? "Active" : "Inactive";
    
    public string FormattedPrereqs => 
        Prerequisites.Count > 0 ? string.Join(", ", Prerequisites) : "None";
}