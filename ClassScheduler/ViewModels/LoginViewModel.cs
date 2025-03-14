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
    public ICommand LoginCommand { get; }
    
    public LoginViewModel()
    {
        LoginCommand = new RelayCommand(Login);
    }
    private void Login()
    {
        if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
        {
            Console.WriteLine("Email or password is empty");
            ErrorMessage = "Email or password is empty";
            return;
        }

        if (Email == "student@wsu.edu" && Password == "password")
        {
            Console.WriteLine("Student logged in");
        } 
        else if (Email == "admin@wsu.edu" && Password == "password")
        {
            Console.WriteLine("Admin logged in");
        }
        else
        {
            Console.WriteLine("Invalid email or password");
            ErrorMessage = "Invalid email or password";
        }
    }
}