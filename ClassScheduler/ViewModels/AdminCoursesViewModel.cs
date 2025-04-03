using System.Collections.Generic;
using ClassScheduler.CoreUI;
using ClassScheduler.Data;
using ClassScheduler.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ClassScheduler.ViewModels;

/// <summary>
/// ViewModel for the Admin Courses page, responsible for managing the list of courses displayed.
/// Shown as a sub view (CurrView) in the window.
/// </summary>
public partial class AdminCoursesViewModel : ViewModelBase
{
    private readonly INavigationService _navigation;
    public List<Course> Courses { get; }

    public AdminCoursesViewModel(INavigationService navigation)
    {
        _navigation = navigation;
        Courses = SystemManager.Courses;
    }

    [RelayCommand]
    private void EditCourses()
    {
 
    }
}