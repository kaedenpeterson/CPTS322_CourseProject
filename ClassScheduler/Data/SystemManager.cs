using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClassScheduler.Models;
using Microsoft.VisualBasic.FileIO;

namespace ClassScheduler.Data;

/// <summary>
/// Static class for system management. Used for getting information and storing records.
/// </summary>
public static class SystemManager
{
    public static readonly List<Course> Courses = [];
    public static readonly List<Student> Students = [];
    private static readonly List<Admin> Admins = [];
    
    static SystemManager() { }

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
        Console.WriteLine("[DEBUG] SystemManager.PullData() called");
        
        using (var parser = new TextFieldParser("course_data.csv"))
        {
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
            parser.HasFieldsEnclosedInQuotes = true;
            
            parser.ReadLine();

            while (!parser.EndOfData)
            {
                var values = parser.ReadFields();
                
                var days = values[11].Contains('|') ? values[11].Split('|') : [];

                List<DayOfWeek> dayOfWeeks = [];
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

                var startTime = DateTime.ParseExact(values[12].Trim(), "h:mmtt", null).TimeOfDay;
                var endTime = DateTime.ParseExact(values[13].Trim(), "h:mmtt", null).TimeOfDay;
                var startDate = DateTime.ParseExact(values[9].Trim(), "MM/dd/yyyy", null);
                var endDate = DateTime.ParseExact(values[10].Trim(), "MM/dd/yyyy", null);

                var schedule = new Schedule(
                    dayOfWeeks,
                    startTime,
                    endTime,
                    startDate,
                    endDate
                );

                var prereqs = values[5].Split('|', StringSplitOptions.RemoveEmptyEntries)
                    .Where(p => p != "N/A").ToList();

                var course = new Course(
                    values[0], values[1], values[2], values[3],
                    int.Parse(values[4]), prereqs,
                    int.Parse(values[6]), values[7],
                    bool.Parse(values[8]), schedule
                );

                Courses.Add(course);
            }
        }

        using (var reader = new StreamReader("user_data.csv"))
        {
            while (reader.ReadLine() is { } line)
            {
                var values = line.Split(',');

                if (values.Length == 0 || string.IsNullOrWhiteSpace(values[0]))
                    continue;

                if (values[0].Equals("Student", StringComparison.OrdinalIgnoreCase))
                {
                    var enrolledCourses = values[7] != "N/A"
                        ? values[7].Split('|').Select(code => Courses.FirstOrDefault(c => c.Code == code)).Where(c => c != null).ToList() 
                        : [];
                    
                    var cartCourses = values[8] != "N/A"
                        ? values[8].Split('|').Select(code => Courses.FirstOrDefault(c => c.Code == code)).Where(c => c != null).ToList()
                        : [];

                    var pastCourses = values[9] != "N/A"
                        ? values[9].Split('|').Select(code => Courses.FirstOrDefault(c => c.Code == code)).Where(c => c != null).ToList()
                        : [];

                    var student = new Student(
                        values[1], values[2], values[3], values[4],
                        int.Parse(values[5]), double.Parse(values[6]),
                        enrolledCourses, cartCourses, pastCourses
                    );

                    Students.Add(student);
                }
                else if (values[0].Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    var admin = new Admin(values[1], values[2], values[3]);
                    Admins.Add(admin);
                }
            }
        }
}

    // PushData() will save the current system data to the database .csv files
    public static void PushData()
    {
        Console.WriteLine("[DEBUG] SystemManager.PushData() called");
        
        using (var writer = new StreamWriter("course_data.csv"))
        {
            writer.WriteLine("Code,Title,Instructor,Description,Credits,Prerequisites," +
                             "Capacity,Location,IsActive,StartDate,EndDate," +
                             "Days,StartTime,EndTime");
            foreach (var course in Courses)
            {
                var prerequisites = course.Prerequisites.Any()
                    ? string.Join('|', course.Prerequisites)
                    : "N/A";
                var days = course.Schedule.Days.Any()
                    ? string.Join('|', course.Schedule.Days.Select(d => d.ToString()[..3]))
                    : "N/A";
                writer.WriteLine($"{course.Code},{course.Name},{course.Instructor},\"{course.Description}\",{course.Credits},{prerequisites}," +
                                 $"{course.MaxSeats},{course.Location},{course.IsActive},{course.Schedule.FormattedStartDate},{course.Schedule.FormattedEndDate}," +
                                 $"{days},{course.Schedule.FormatTime(course.Schedule.StartTime)},{course.Schedule.FormatTime(course.Schedule.EndTime)}");
            }
        }
           

        using (var writer = new StreamWriter("user_data.csv"))
        {
            writer.WriteLine("Role,Email,Password,Name,StudentID,TotalCredits,GPA,Courses,CartCourses,PastCourses");
            foreach (var admin in Admins)
            {
                writer.WriteLine($"Admin,{admin.Email},{admin.Password},{admin.Name},N/A,N/A,N/A,N/A,N/A,N/A");
            }
            foreach (var student in Students)
            {
                var courses = student.Courses.Any()
                    ? string.Join('|', student.Courses.Select(p => p.Code))
                    : "N/A";
                var cartCourses = student.CartCourses.Any()
                    ? string.Join('|', student.CartCourses.Select(p => p.Code))
                    : "N/A";
                var pastCourses = student.PastCourses.Any()
                    ? string.Join('|', student.PastCourses.Select(p => p.Code))
                    : "N/A";
                writer.WriteLine($"Student,{student.Email},{student.Password},{student.Name}," +
                                 $"{student.StudentId},{student.TotalCredits},{student.Gpa},{courses},{cartCourses},{pastCourses}");
            }
        }
    }
}