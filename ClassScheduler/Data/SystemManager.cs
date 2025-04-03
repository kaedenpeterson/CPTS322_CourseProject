using System;
using System.Collections.Generic;
using System.IO;
using ClassScheduler.Models;

namespace ClassScheduler.Data;

/// <summary>
/// Static class for system management. Used for getting information and storing records.
/// </summary>
public static class SystemManager
{
    private static readonly string UserDataFile;
    private static readonly string CourseDataFile;

    private static readonly List<Student> Students = [];
    private static readonly List<Admin> Admins = [];

    public static readonly List<Course> Courses = [];

    static SystemManager()
    {
        UserDataFile = "user_data.csv";
        CourseDataFile = "course_data.csv";
        PullData();

        // Adding to lists through constructor because database handling is not implemented yet

        var cpts101 = new Course(
            "CPTS 101", "Intro to Computer Science", "Parteek Kumar",
            "Introduction to programs within the School of Electrical Engineering" +
            " and Computer Science discussing resources, opportunities, and knowledge and skills" +
            " necessary to succeed within EECS majors.",
            1, [], 120, "Cleveland Hall 30", false,
            new Schedule([DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday],
                new TimeSpan(10, 10, 0), new TimeSpan(11, 0, 0),
                new DateTime(2025, 3, 23), new DateTime(2025, 5, 1)), []);

        var testStudent = new Student
            ("test", "pass", "Firstname Lastname", "01", [cpts101, cpts101, cpts101, cpts101, cpts101]);
        Students.Add(testStudent);

        var testAdmin = new Admin
            ("test", "pass", "Firstname Lastname");
        Admins.Add(testAdmin);

        cpts101.Prerequisites.Add(cpts101);
        cpts101.Prerequisites.Add(cpts101);
        cpts101.Prerequisites.Add(cpts101);
        cpts101.EnrolledStudents.Add(testStudent);
        Courses.Add(cpts101);
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
        using (StreamWriter writer = new StreamWriter(CourseDataFile))
        {
            writer.WriteLine("CourseID,Title,Instructor,Description,Credits,maxSeats,Location,isActive,StartDate,EndDate,Days,StartTime,EndTime");
            foreach (var course in Courses)
            {
                //string days = string.Join("|", course.Schedule.Days);
                writer.WriteLine($"{course.Code},{course.Name},{course.Instructor},\"{course.Description}\",{course.Credits},{course.MaxSeats},{course.Location},{course.IsActive},{course.Schedule.FormattedStartDate:yyyy-MM-dd},{course.Schedule.FormattedEndDate:yyyy-MM-dd}");
            }
        }

        using (StreamWriter writer = new StreamWriter(UserDataFile))
        {
            writer.WriteLine("email, name, password, typeOfUser");
            foreach (var admin in Admins)
            {
                writer.WriteLine($"{admin.Email}, {admin.Name}, {admin.Password}, admin");
            }
            foreach (var student in Students)
            {
                writer.WriteLine($"{student.Email}, {student.Name}, {student.Password}, ");
            }
        }
    }
}