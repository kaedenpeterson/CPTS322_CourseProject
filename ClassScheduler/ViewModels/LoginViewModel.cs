using ClassScheduler.CoreUI;
using ClassScheduler.CoreUI.Admin;
using ClassScheduler.CoreUI.Student;
using ClassScheduler.Data;
using ClassScheduler.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ClassScheduler.ViewModels;

/// <summary>
/// ViewModel for the login page. Provides logic for user login.
/// </summary>
public partial class LoginViewModel(INavigationService navigation) : ViewModelBase
{
    [ObservableProperty] private string _email = string.Empty;
    [ObservableProperty] private string _password = string.Empty; 
    [ObservableProperty] private string _errorMessage = string.Empty;
    [ObservableProperty] private string? _selectedRole;

    [RelayCommand]
    private void SetRole(string? role)
    {
        SelectedRole = role;
    }
    
    [RelayCommand]
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
        else ErrorMessage = "Invalid email or password";
    }

    private void OpenDashboard()
    {
        switch (SelectedRole)
        {
            case "Student":
                var student = SystemManager.GetStudent(Email);
                if (student != null) navigation.SwitchTo<StudentRootView>(student);
                
                break;
            case "Admin":
                var admin = SystemManager.GetAdmin(Email);
                if (admin != null) navigation.SwitchTo<AdminRootView>(admin);
                
                break;
        }
    }

}