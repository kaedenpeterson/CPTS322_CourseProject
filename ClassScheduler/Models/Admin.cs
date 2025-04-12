namespace ClassScheduler.Models;

/// <summary>
/// Represents an admin. Inherits from user. Includes admin-specific properties.
/// </summary>
public class Admin : User
{
    public Admin(string email, string password, string name) 
        : base(email, password, name) { }
}