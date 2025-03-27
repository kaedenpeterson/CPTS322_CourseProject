using System.Collections.Generic;
using ClassScheduler.CoreUI;
using ClassScheduler.Data;
using ClassScheduler.Models;

namespace ClassScheduler.ViewModels;

/// <summary>
/// ViewModel for the Courses page, responsible for managing the list of courses displayed.
/// Shown as a sub view (CurrView) in the window.
/// </summary>
public partial class CoursesViewModel : ViewModelBase
{
    private readonly INavigationService _navigation;
    public List<Course> Courses { get; }

    public CoursesViewModel(INavigationService navigation)
    { 
        _navigation = navigation;
        Courses = SystemManager.Courses;
    }
}