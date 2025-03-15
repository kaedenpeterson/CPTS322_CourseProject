/*
Description: Represents an admin. Inherits from user. Includes admin-specific properties.
Author: Kaeden Peterson 11858249
Date: 3-14-25
*/

namespace ClassScheduler.Models;

public class Admin : User
{
    public Admin(string email, string password, string name) 
        : base(email, password, name) { }
}