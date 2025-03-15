/*
Description: Abstract class for user. Used for each user role (Student, admin).
Author: Kaeden Peterson 11858249
Date: 3-14-25
*/

namespace ClassScheduler.Models;
public abstract class User
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }

    protected User(string email, string password, string name)
    {
        Email = email;
        Password = password;
        Name = name;
    }
    
}