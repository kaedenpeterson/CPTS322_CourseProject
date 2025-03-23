/*
Description: Represents a course in the system. Stores course details.
Author: Kaeden Peterson 11858249
Date: 3-17-25
*/

using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ClassScheduler.Models;

public class Course
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Instructor { get; set; }
    public string Description { get; set; }
    public int Credits { get; set; }
    public string Prerequisites { get; set; }
    public int OpenSeats { get; set; }
    public int MaxSeats { get; set; }
    
    public string Location {get; set;}
    
    public bool IsActive { get; set; }
    public Schedule Schedule { get; set; }
    

    public Course(string code, string name, string instructor, string description, 
        int credits, string prerequisites, int openSeats, int maxSeats, string location,
        bool isActive, Schedule schedule)
    {
        Code = code;
        Name = name;
        Instructor = instructor;
        Description = description;
        Credits = credits;
        Prerequisites = prerequisites;
        OpenSeats = openSeats;
        MaxSeats = maxSeats;
        Location = location;
        IsActive = isActive;
        Schedule = schedule;
    }
}