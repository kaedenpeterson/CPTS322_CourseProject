/*
Description: Static class for system management. Used for getting information and storing records.
Author: Kaeden Peterson 11858249
Date: 3-14-25
*/

using System;
using System.Collections.Generic;
using ClassScheduler.Models;

namespace ClassScheduler.Data;

public static class SystemManager
{
    private static readonly string UserDataFile;
    private static readonly string CourseDataFile;
    
    private static readonly List<Student> Students = [];
    private static readonly List<Admin> Admins = [];
    
    private static readonly List<Course> Courses = [];

    static SystemManager()
    {
        UserDataFile = "user_data.csv";
        CourseDataFile = "course_data.csv";
        PullData();
        
        var cpts101 = new Course(
            "CPTS 101", "Intro to Computer Science", "Parteek Kumar",
            "Introduction to programs within the School of Electrical Engineering" +
            " and Computer Science discussing resources, opportunities, and knowledge and skills" +
            " necessary to succeed within EECS majors.", 
            1, "MATH 101, ENGLISH 101", 119, 120, "Cleveland Hall 30", true,
            new Schedule(new List<DayOfWeek>{ DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday },
                new TimeSpan(10, 10, 0), new TimeSpan(11, 0, 0), 
                     new DateTime(2025, 3, 23), new DateTime(2025, 5, 1)));
        
        Courses.Add(cpts101);
        
        // Dummy student for testing login
        Students.Add(new Student
            ("test", "pass", "Firstname Lastname", "01", [cpts101, cpts101, cpts101, cpts101, cpts101] ));
                
        Admins.Add(new Admin
            ("test", "pass", "Firstname Lastname"));
    }
      
    public static bool IsValidCredentials(string role, string email, string password)
    {
        switch (role)
        {
            case "Student":
                var student = GetStudent(email);
                return student is not null && student.Password == password;
            case "Admin":
                var admin = GetAdmin(email);
                return admin is not null && admin.Password == password;
            default:
                return false;
        }
    }
    
    public static Student? GetStudent(string email)
    {
        return Students.Find(student => student.Email == email);
    }

    public static Admin? GetAdmin(string email)
    {
        return Admins.Find(admin => admin.Email == email);
    }

    // PullData() will load data from the database .csv files and populate the lists
    public static void PullData() 
    {
        
    }

    // PushData() will save the current system data to the database .csv files
    public static void PushData()
    {
        
    }
}