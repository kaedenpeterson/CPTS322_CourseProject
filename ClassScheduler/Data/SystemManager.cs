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

    public static readonly List<Student> Students = [];
    private static readonly List<Admin> Admins = [];

    public static readonly List<Course> Courses = [];

    static SystemManager()
    {
        PullData();
        //Adding to lists through constructor because database handling is not implemented yet
        
        /*
        var cpts101 = new Course(
            "CPTS 101", "Intro to Computer Science", "Parteek Kumar",
            "Introduction to programs within the School of Electrical Engineering" +
            " and Computer Science discussing resources and knowledge and skills", 1, 
            [], 120, "Cleveland Hall 30", false,
            new Schedule([DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday],
                new TimeSpan(10, 10, 0), new TimeSpan(11, 0, 0),
                new DateTime(2025, 3, 23), new DateTime(2025, 5, 1)));

        var testStudent = new Student
            ("test", "pass", "Firstname Lastname", "01", 10, 2.4, [cpts101, cpts101, cpts101, cpts101, cpts101], [cpts101]);
        Students.Add(testStudent);

        var testAdmin = new Admin
            ("test", "pass", "Firstname Lastname");
        Admins.Add(testAdmin);

        cpts101.Prerequisites.Add("CPTS 101");
        cpts101.Prerequisites.Add("CPTS 101");
        cpts101.Prerequisites.Add("CPTS 101");
        Courses.Add(cpts101);
        */
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
    private static void PullData()
    {
        using (StreamReader reader = new StreamReader("course_data.csv"))
        {
            reader.ReadLine();

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] values = line.Split(',');

                string[] days = values[11].Contains("|") ? values[11].Split('|') : [];

                List<DayOfWeek> dayOfWeeks = new();
                foreach (var day in days)
                {
                    switch (day.Trim())
                    {
                        case "Mon": dayOfWeeks.Add(DayOfWeek.Monday); break;
                        case "Tue": dayOfWeeks.Add(DayOfWeek.Tuesday); break;
                        case "Wed": dayOfWeeks.Add(DayOfWeek.Wednesday); break;
                        case "Thu": dayOfWeeks.Add(DayOfWeek.Thursday); break;
                        case "Fri": dayOfWeeks.Add(DayOfWeek.Friday); break;
                    }
                }

                DateTime startTime = DateTime.Parse(values[12]);
                DateTime endTime = DateTime.Parse(values[13]);
                DateTime startDate = DateTime.Parse(values[9]);
                DateTime endDate = DateTime.Parse(values[10]);

                Schedule schedule = new Schedule(
                    dayOfWeeks,
                    startTime.TimeOfDay,
                    endTime.TimeOfDay,
                    startDate,
                    endDate
                );

                List<string> prereqs = values[5].Split('|', StringSplitOptions.RemoveEmptyEntries)
                    .Where(p => p != "N/A").ToList();

                var course = new Course(values[0], values[1], values[2], values[3],
                    int.Parse(values[4]), prereqs,
                    int.Parse(values[6]), values[7],
                    bool.Parse(values[8]), schedule);

                Courses.Add(course);
            }
        }

        using (StreamReader reader = new StreamReader("user_data.csv"))
        {
            string? line = reader.ReadLine();

            while ((line = reader.ReadLine()) != null)
            {
                string[] values = line.Split(',');

                if (values.Length == 0 || string.IsNullOrWhiteSpace(values[0]))
                    continue;

                if (values[0].Equals("Student", StringComparison.OrdinalIgnoreCase))
                {
                    if (values.Length < 9)
                    {
                        Console.WriteLine("Invalid student data line: " + line);
                        continue;
                    }
                
                    List<Course?> enrolledCourses = values[7] != "N/A"
                        ? values[7].Split('|').Select(code => Courses.FirstOrDefault(c => c.Code == code)).Where(c => c != null).ToList()!
                        : new();

                    List<Course?> pastCourses = values[8] != "N/A"
                        ? values[8].Split('|').Select(code => Courses.FirstOrDefault(c => c.Code == code)).Where(c => c != null).ToList()!
                        : new();

                    var student = new Student(
                        values[1], values[2], values[3], values[4],
                        int.Parse(values[5]), double.Parse(values[6]),
                        enrolledCourses, pastCourses
                    );

                    Students.Add(student);
                }
                else if (values[0].Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    if (values.Length < 4)
                    {
                        Console.WriteLine("Invalid admin data line: " + line);
                        continue;
                    }

                    var admin = new Admin(values[1], values[2], values[3]);
                    Admins.Add(admin);
                }
            }
        }
}

    // PushData() will save the current system data to the database .csv files
    public static void PushData()
    {
        using (StreamWriter writer = new StreamWriter("course_data.csv"))
        {
            writer.WriteLine("CourseID,Title,Instructor,Description,Credits,Prerequisites," +
                             "maxSeats,Location,isActive,StartDate,EndDate," +
                             "Days,StartTime,EndTime");
            foreach (var course in Courses)
            {
                var prerequisites = course.Prerequisites.Any()
                    ? string.Join("|", course.Prerequisites)
                    : "N/A";
                var days = course.Schedule.Days.Any()
                    ? string.Join("|", course.Schedule.Days.Select(d => d.ToString()[..3]))
                    : "N/A";
                writer.WriteLine($"{course.Code},{course.Name},{course.Instructor},{course.Description},{course.Credits},{prerequisites}," +
                                 $"{course.MaxSeats},{course.Location},{course.IsActive},{course.Schedule.FormattedStartDate},{course.Schedule.FormattedEndDate}," +
                                 $"{days},{course.Schedule.FormatTime(course.Schedule.StartTime)},{course.Schedule.FormatTime(course.Schedule.EndTime)}");
            }
        }
           

        using (StreamWriter writer = new StreamWriter("user_data.csv"))
        {
            writer.WriteLine("typeOfUser,email,password,name,studentId,totalCredits,GPA,courses,pastCourses");
            foreach (var admin in Admins)
            {
                writer.WriteLine($"Admin,{admin.Email},{admin.Password},{admin.Name},N/A,N/A,N/A,N/A,N/A");
            }
            foreach (var student in Students)
            {
                var courses = student.Courses.Any()
                    ? string.Join("|", student.Courses.Select(p => p.Code))
                    : "N/A";
                var pastCourses = student.PastCourses.Any()
                    ? string.Join("|", student.PastCourses.Select(p => p.Code))
                    : "N/A";
                writer.WriteLine($"Student,{student.Email},{student.Password},{student.Name},{student.StudentId},{student.TotalCredits},{student.Gpa},{courses},{pastCourses}");
            }
        }
    }
}