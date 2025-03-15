/*
Description: ViewModel for login page in ClassScheduler. Handles user authentication logic and data binding.
Author: Kaeden Peterson 11858249
Date: 3-14-25
*/

using System;
using System.Windows.Input;
using Avalonia.Controls;
using ClassScheduler.Data;
using ClassScheduler.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ClassScheduler.ViewModels;

public partial class LoginViewModel : ViewModelBase
{
    public event Action? LoginSuccess;
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
            ErrorMessage = "Empty email or password";
            return;
        }

        if (SystemManager.IsValidCredentials(SelectedRole, Email, Password))
        {
            ErrorMessage = string.Empty;
            OpenDashboard();
        }
        else
        {
            ErrorMessage = "Invalid email or password";
        }
    }

    private void OpenDashboard()
    {
        Window? dashboardWindow = null;
        switch (SelectedRole)
        {
            case "Student":
                var student = SystemManager.GetStudent(Email);
                if (student is not null)
                {
                    dashboardWindow = new StudentView(student);
                }

                break;
            case "Admin":
                var admin = SystemManager.GetAdmin(Email);
                if (admin is not null)
                {
                    dashboardWindow = new AdminView(admin);
                }
                
                break;
        }
        dashboardWindow?.Show();
        LoginSuccess?.Invoke();
    }

}