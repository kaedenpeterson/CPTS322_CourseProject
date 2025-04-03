using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            ("test", "pass", "Firstname Lastname", "01", 10, 2.0, [cpts101, cpts101, cpts101, cpts101, cpts101], [cpts101]);
        Students.Add(testStudent);

        var testAdmin = new Admin
            ("test", "pass", "Firstname Lastname");
        Admins.Add(testAdmin);

        cpts101.Prerequisites.Add(cpts101);
        cpts101.Prerequisites.Add(cpts101);
        cpts101.Prerequisites.Add(cpts101);
        cpts101.EnrolledStudents.Add(testStudent);
        Courses.Add(cpts101); 
        
        PushData();
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
        //using (StreamReader reader = new StreamReader(CourseDataFile))
        //{
        //    while (!reader.EndOfStream)
        //    {
        //        string line = reader.ReadLine();
        //        string[] values = line.Split(',');

        //        List<Course> prereqs = values[5]; // this depends on how the prereqs are printed to the csv

        //        Schedule schedule = new Schedule(); // assembling this seems to be impossible based on how the schedule is made in the first place

        //        var course = new Course(values[0], values[1], values[2], values[3], int.Parse(values[4]), prereqs, int.Parse(values[6]), values[7], bool.Parse(values[8]), values[9], values[10]);
        //        Courses.Add(course);
        //    }
        //}
    }

    // PushData() will save the current system data to the database .csv files
    public static void PushData()
    {
        using (StreamWriter writer = new StreamWriter("course_data.csv"))
        {
            writer.WriteLine("CourseID,Title,Instructor,Description,Credits,Prerequisites," +
                             "maxSeats,Location,isActive,StartDate,EndDate," +
                             "Days,StartTime,EndTime,EnrolledStudents");
            foreach (var course in Courses)
            {
                var prerequisites = course.Prerequisites.Any()
                    ? string.Join("|", course.Prerequisites.Select(p => p.Code))
                    : "N/A";
                var enrolledStudents = course.EnrolledStudents.Any()
                    ? string.Join("|", course.EnrolledStudents.Select(s => s.Name))
                    : "N/A";
                var days = course.Schedule.Days.Any()
                    ? string.Join("|", course.Schedule.Days.Select(d => d.ToString()[..3]))
                    : "N/A";
                writer.WriteLine($"{course.Code},{course.Name},{course.Instructor},\"{course.Description}\",{course.Credits},{prerequisites}," +
                                 $"{course.MaxSeats},{course.Location},{course.IsActive},{course.Schedule.FormattedStartDate},{course.Schedule.FormattedEndDate}," +
                                 $"{days},{course.Schedule.FormatTime(course.Schedule.StartTime)},{course.Schedule.FormatTime(course.Schedule.EndTime)},{enrolledStudents}");
            }
        }

        using (StreamWriter writer = new("user_data.csv"))
        {
            writer.WriteLine("typeOfUser,email,password,name,studentId,courses,pastCourses,totalCredits,GPA");
            foreach (var admin in Admins)
            {
                writer.WriteLine($"Admin,{admin.Email},{admin.Password},{admin.Name},N/A,N/A,N/A,N/A,N/A");
            }
            foreach (var student in Students)
            {
                writer.WriteLine($"Student,{student.Email},{student.Password},{student.Name},{student.StudentId}"); // Add rest of fields and write them to file just like with the course data
            }
        }
    }
}