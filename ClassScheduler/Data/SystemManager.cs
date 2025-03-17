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
    private static readonly string DatabaseFile = "database.csv";
    private static readonly List<Student> Students = [];
    private static readonly List<Admin> Admins = [];
    
    static SystemManager()
    {
        LoadData();
        
        // Dummy student for testing login
        Students.Add(new Student
            ("test@wsu.edu", "password123", "Student Name", "0", ["CPTS_121"] ));

        Admins.Add(new Admin
            ("admin@wsu.edu", "password", "Admin Name"));
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

    // LoadData() will load data from the database .csv file and populate the lists
    public static void LoadData() 
    {
        
    }
}