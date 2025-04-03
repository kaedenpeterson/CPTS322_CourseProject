using System.ComponentModel;
using Avalonia.Controls;
using ClassScheduler.CoreUI.Admin;
using ClassScheduler.CoreUI.Student;
using ClassScheduler.Views;

namespace ClassScheduler.CoreUI;

/// <summary>
/// Responsible for managing and switching between views. Holds the <see cref="MainView"/>
/// (switches between the login page and logged in state) and the <see cref="CurrView"/>
/// (switches between different sub views when in logged in state).
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
            if (_mainView == value) return;
            _mainView = value;
            OnPropertyChanged(nameof(MainView));
        }
    }
    
    private UserControl _currView;
    public UserControl CurrView
    {
        get => _currView;
        set
        {
            if (_currView == value) return;
            _currView = value;
            OnPropertyChanged(nameof(CurrView));
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
    
    public void SwitchTo<T>(object? parameter = null) where T : UserControl
    {
        if (typeof(T) == typeof(StudentRootView) && parameter is Models.Student student)
        {
            MainView = new StudentRootView(this, student);
            CurrView = new StudentView(this, student);
        }
        else if (typeof(T) == typeof(AdminRootView) && parameter is Models.Admin admin)
        {
            MainView = new AdminRootView(this, admin);
            CurrView = new AdminView(this, admin);
        }
        else if (typeof(T) == typeof(StudentView) && parameter is Models.Student s) 
            CurrView = new StudentView(this, s);
        
        else if (typeof(T) == typeof(AdminView) && parameter is Models.Admin a)
            CurrView = new AdminView(this, a);
        
        else if (typeof(T) == typeof(CoursesView))
            CurrView = new CoursesView(this);

        else if (typeof(T) == typeof(AdminCoursesView))
            CurrView = new AdminCoursesView(this);

        else if (typeof(T) == typeof(LoginView))
            MainView = new LoginView(this);
    }
}