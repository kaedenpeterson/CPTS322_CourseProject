using System.Collections.Generic;

namespace ClassScheduler.Models;

/// <summary>
/// Represents a student. Inherits from user. Includes student-specific properties.
/// </summary>
public class Student : User
{
    public List<Course> Courses { get; set; }
    public List<Course> PastCourses { get; set; }
    
    public List<Course> Cart { get; set; } = [];
    
    // Below properties will be shown on profile page
    public string StudentId { get; set; }
    
    public int TotalCredits { get; set; }
    
    public int CurrentCredits { get; set; } // Will be calculated by adding up credits of enrolled classes, does not need to be set through constructor
    
    public double Gpa { get; set; }
    
    public Student(string email, string password, string name, string studentId, int totalCredits, double gpa, List<Course?> courses, List<Course?> pastCourses) 
        : base(email, password, name)
    {
        StudentId = studentId;
        TotalCredits = totalCredits;
        Gpa = gpa;
        Courses = courses;
        PastCourses = pastCourses;
    }
}