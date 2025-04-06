namespace ClassScheduler.Models;

/// <summary>
/// Abstract class for user. Used for each user role (Student, admin).
/// </summary>
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