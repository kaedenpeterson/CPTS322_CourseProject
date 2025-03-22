using Avalonia.Controls;
using ClassScheduler.Models;
using ClassScheduler.Views;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ClassScheduler.ViewModels;

public partial class ViewManager : ViewModelBase
{
    [ObservableProperty] private UserControl _currView;

    private readonly StudentView studentView;
    private readonly AdminView adminView;
    private readonly CoursesView coursesView;

    private string UserRole { get; }

    public ViewManager(string userRole = "default", Student? student = null, Admin? admin = null)
    {
        UserRole = userRole;
        coursesView = new CoursesView(this);

        CurrView = new LoginView(this);
        
        studentView = student is not null ? new StudentView(student, this) : null!;
        adminView = admin is not null ? new AdminView(admin, this) : null!;

        CurrView = UserRole switch
        {
            "Student" when student is not null => studentView,
            "Admin" when admin is not null => adminView,
            _ => CurrView
        };
    }
    
    public void ChangeView(UserControl newView)
    {
        CurrView = newView;
    }
}