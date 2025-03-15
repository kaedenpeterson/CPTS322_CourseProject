/*
Description: Represents a student. Inherits from user. Includes student-specific properties.
Author: Kaeden Peterson 11858249
Date: 3-14-25
*/

using System.Collections.Generic;

namespace ClassScheduler.Models;

public class Student : User
{
    public string StudentID { get; set; }
    
    public List<string> Courses { get; set; }
    
    public Student(string email, string password, string name, string studentId, List<string> courses) 
        : base(email, password, name)
    {
        StudentID = studentId;
        Courses = courses;
    }
}