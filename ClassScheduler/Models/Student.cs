using System.Collections.Generic;
using System.Linq;

namespace ClassScheduler.Models;

/// <summary>
/// Represents a student. Inherits from user. Includes student-specific properties.
/// </summary>
public class Student : User
{
    public List<Course> EnrolledCourses { get; set; }
    public List<Course> PastCourses { get; set; }

    public List<Course> CartCourses { get; set; }
    
    // Below properties will be shown on profile page
    public string StudentId { get; set; }
    
    public int TotalCredits { get; set; }
    
    public int CurrentCredits => EnrolledCourses.Sum(c => c.Credits);
    
    public double Gpa { get; set; }
    
    public Student(string email, string password, string name, string studentId, int totalCredits, double gpa, List<Course> enrolledCourses, List<Course> cartCourses, List<Course> pastCourses) 
        : base(email, password, name)
    {
        StudentId = studentId;
        TotalCredits = totalCredits;
        Gpa = gpa;
        EnrolledCourses = enrolledCourses;
        CartCourses = cartCourses;
        PastCourses = pastCourses;
    }
}