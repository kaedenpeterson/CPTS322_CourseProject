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

        //Adding to lists through constructor because database handling is not implemented yet

        //var cpts101 = new Course(
        //    "CPTS 101", "Intro to Computer Science", "Parteek Kumar",
        //    "Introduction to programs within the School of Electrical Engineering" +
        //    " and Computer Science discussing resources and knowledge and skills" +
        //    " necessary to succeed within EECS majors.",
        //    1, [], 120, "Cleveland Hall 30", false,
        //    new Schedule([DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday],
        //        new TimeSpan(10, 10, 0), new TimeSpan(11, 0, 0),
        //        new DateTime(2025, 3, 23), new DateTime(2025, 5, 1)), []);

        //var testStudent = new Student
        //    ("test", "pass", "Firstname Lastname", "01", 10, 2.0, [cpts101, cpts101, cpts101, cpts101, cpts101], [cpts101]);
        //Students.Add(testStudent);

        //var testAdmin = new Admin
        //    ("test", "pass", "Firstname Lastname");
        //Admins.Add(testAdmin);

        //cpts101.Prerequisites.Add(cpts101);
        //cpts101.Prerequisites.Add(cpts101);
        //cpts101.Prerequisites.Add(cpts101);
        //cpts101.EnrolledStudents.Add(testStudent);
        //Courses.Add(cpts101);

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
        using (StreamReader reader = new StreamReader("user_data.csv"))
        {
            string? line;

            // Skip header if you have one
            line = reader.ReadLine();

            while ((line = reader.ReadLine()) != null)
            {
                string[] values = line.Split(',');

                if (values.Length == 0 || string.IsNullOrWhiteSpace(values[0]))
                    continue;

                if (values[0].Equals("Student", StringComparison.OrdinalIgnoreCase))
                {
                    if (values.Length < 7)
                    {
                        Console.WriteLine("Invalid student data line: " + line);
                        continue;
                    }                  
                    List<Course> enrolledCourses = new();
                    List<Course> pastCourses = new();
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

        using (StreamReader reader = new StreamReader("course_data.csv"))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                line = reader.ReadLine();
                string[] values = line.Split(',');

                string[] days = [];
                if (values[11].Contains("|"))
                {
                    days = values[11].Split('|');
                }
                string startTimeStr = values[12];      
                string endTimeStr = values[13];        
                string startDateStr = values[9];      
                string endDateStr = values[10];        

                List<DayOfWeek> dayOfWeeks = []; // I don't think this actually works, but I don't know how to make it work
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

                DateTime startTime = DateTime.Parse(startTimeStr); 
                DateTime endTime = DateTime.Parse(endTimeStr);     

                DateTime startDate = DateTime.Parse(startDateStr); 
                DateTime endDate = DateTime.Parse(endDateStr);     

                Schedule schedule = new Schedule(
                    dayOfWeeks,
                    startTime.TimeOfDay,
                    endTime.TimeOfDay,
                    startDate,
                    endDate
                );


                List<Course> prereqs = [];
                List<Student> enrolledStudents = [];
                //I do not think this is literally possible to make, because prereqs is a list of courses, to add it to the list I would need to take the string "CPTS101" for example
                //and literally assemble an entire class out of that

                //down below is an idea for how to get the previous students
                //because student.Courses is a list of courses, to run a "contains" it needs to match with a course variable type but of course I only have the ID value which is a string, so I'm not sure how to make it work

                //foreach (var student in Students)
                //{
                //    if (student.Courses.Contains(values[0])) {
                //        enrolledStudents.Add(student);
                //    }
                //}

                var course = new Course(values[0], values[1], values[2], values[3], int.Parse(values[4]), prereqs, int.Parse(values[6]), values[7], bool.Parse(values[8]), schedule, enrolledStudents);
                Courses.Add(course);
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
                writer.WriteLine($"{course.Code},{course.Name},{course.Instructor},{course.Description},{course.Credits},{prerequisites}," +
                                 $"{course.MaxSeats},{course.Location},{course.IsActive},{course.Schedule.FormattedStartDate},{course.Schedule.FormattedEndDate}," +
                                 $"{days},{course.Schedule.FormatTime(course.Schedule.StartTime)},{course.Schedule.FormatTime(course.Schedule.EndTime)},{enrolledStudents}");
            }
        }
           

        using (StreamWriter writer = new StreamWriter("user_data.csv"))
        {
            writer.WriteLine("typeOfUser,email,password,name,studentId,totalCredits,GPA,course,pastcourses");
            foreach (var admin in Admins)
            {
                writer.WriteLine($"Admin,{admin.Email},{admin.Password},{admin.Name},N/A,N/A,N/A,N/A,N/A,N/A,N/A");
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