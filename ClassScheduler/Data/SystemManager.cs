/*
Description: Static class for system management. Used for getting information and storing records.
Author: Kaeden Peterson 11858249
Date: 3-14-25
*/

using System.Collections.Generic;
using ClassScheduler.Models;

namespace ClassScheduler.Data;

public static class SystemManager
{
    private static readonly string UserDataFile;
    private static readonly string CourseDataFile;
    private static readonly List<Student> Students = [];
    private static readonly List<Admin> Admins = [];

    static SystemManager()
    {
        UserDataFile = "user_data.csv";
        CourseDataFile = "course_data.csv";
        PullData();
        
        // Dummy student for testing login
        Students.Add(new Student
            ("test", "pass", "Kaeden Peterson", "01", [null!] ));
                
        Admins.Add(new Admin
            ("test", "pass", "Kaeden Peterson"));
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