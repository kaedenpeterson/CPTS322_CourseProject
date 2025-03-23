using System.ComponentModel;
using System.Net.Mime;
using Avalonia.Controls;
using Avalonia.Platform;
using ClassScheduler.CoreUI.Student;
using ClassScheduler.Views;
using ClassScheduler.Models;

namespace ClassScheduler.CoreUI;

/// <summary>
/// Responsible for managing and switching between views. Holds the <see cref="MainView"/>
/// (switches between the login page and dashboard) and the <see cref="CurrView"/>
/// (switches between different sub views such as <see cref="CoursesView"/>).
/// This is the only class responsible for instantiating and switching views.
/// </summary>
public sealed class NavigationService : INavigationService
{
    private UserControl _mainView;
    public UserControl MainView
    {
        get => _mainView;
        set
        {
            if (_mainView != value)
            {
                _mainView = value;
                OnPropertyChanged(nameof(MainView));
            }
        }
    }
    
    private UserControl _currView;
    public UserControl CurrView
    {
        get => _currView;
        set
        {
            if (_currView != value)
            {
                _currView = value;
                OnPropertyChanged(nameof(CurrView));
            }
        }
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public NavigationService()
    {
        MainView = new LoginView(this);
    }
    
    public void NavigateTo<T>(object? parameter = null) where T : UserControl
    {
        if (typeof(T) == typeof(StudentRootView) && parameter is Models.Student student)
        {
            MainView = new StudentRootView(this, student);
        }
        /*
        else if (typeof(T) == typeof(AdminRootView) && parameter is Admin admin)
        {
            MainView = new AdminRootView(admin, this);
        }
        */
        else if (typeof(T) == typeof(StudentView) && parameter is Models.Student aStudent)
        {
            CurrView = new StudentView(aStudent, this);
        }
        else if (typeof(T) == typeof(AdminView) && parameter is Admin admin)
        {
            CurrView = new AdminView(admin, this);
        }
        else if (typeof(T) == typeof(CoursesView))
        {
            CurrView = new CoursesView(this);
        }
        else if (typeof(T) == typeof(LoginView))
        {
            MainView = new LoginView(this);
        }
    }
}