/*
Description: ViewModel for login page in ClassScheduler. Handles user authentication logic and data binding.
Author: Kaeden Peterson 11858249
Date: 3-14-25
*/

using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ClassScheduler.ViewModels;

public partial class LoginViewModel : ViewModelBase
{
    [ObservableProperty] private string _email = string.Empty;
    [ObservableProperty] private string _password = string.Empty; 
    [ObservableProperty] private string _errorMessage = string.Empty;
    [ObservableProperty] private string? _selectedRole;
    public ICommand LoginCommand { get; }
    
    public ICommand SetRoleCommand { get; }

    private void SetRole(string? role)
    {
        SelectedRole = role;
    }
    public LoginViewModel()
    {
        LoginCommand = new RelayCommand(Login);
        SetRoleCommand = new RelayCommand<string>(SetRole);
    }
    private void Login()
    {
        if (SelectedRole is null)
        {
            ErrorMessage = "Please select a role";
            return;
        }
        
        if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
        {
            Console.WriteLine("Empty email or password");
            ErrorMessage = "Empty email or password";
            return;
        }

        if (SelectedRole == "Student" && Email == "student@wsu.edu" && Password == "password")
        {
            Console.WriteLine("Student logged in");
            ErrorMessage = string.Empty;
        } 
        else if (SelectedRole == "Admin" && Email == "admin@wsu.edu" && Password == "password")
        {
            Console.WriteLine("Admin logged in");
            ErrorMessage = string.Empty;
        }
        else
        {
            Console.WriteLine("Invalid email or password");
            ErrorMessage = "Invalid email or password";
        }
    }
}