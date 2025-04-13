using System.ComponentModel;
using Avalonia.Controls;
using ClassScheduler.CoreUI.Admin;
using ClassScheduler.CoreUI.Student;
using ClassScheduler.Data;
using ClassScheduler.Models;
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
        if (typeof(T) == typeof(StudentRootView) && parameter is Models.Student s1)
        {
            MainView = new StudentRootView(this, s1);
            CurrView = new StudentView(this, s1);
        }
        else if (typeof(T) == typeof(AdminRootView) && parameter is Models.Admin a1)
        {
            MainView = new AdminRootView(this, a1);
            CurrView = new AdminView(this, a1);
        }
        else if (typeof(T) == typeof(StudentView) && parameter is Models.Student s2) 
            CurrView = new StudentView(this, s2);
        
        else if (typeof(T) == typeof(AdminView) && parameter is Models.Admin a2)
            CurrView = new AdminView(this, a2);

        else if (typeof(T) == typeof(AdminCoursesView))
            CurrView = new AdminCoursesView(this);
        
        else if (typeof(T) == typeof(StudentCoursesView) && parameter is Models.Student s3)
            CurrView = new StudentCoursesView(this, s3);

        else if (typeof(T) == typeof(CartView) && parameter is Models.Student s4)
            CurrView = new CartView(this, s4);
        
        else if (typeof(T) == typeof(LoginView))
        {
            SystemManager.PushData();
            MainView = new LoginView(this);
        }

        else if (typeof(T) == typeof(EditCourseView) && parameter is Course course)
            CurrView = new EditCourseView(this, course);
    }
}