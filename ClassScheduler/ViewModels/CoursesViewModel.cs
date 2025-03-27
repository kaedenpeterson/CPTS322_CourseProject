using System.Collections.Generic;
using ClassScheduler.CoreUI;
using ClassScheduler.Data;
using ClassScheduler.Models;

namespace ClassScheduler.ViewModels;

public partial class CoursesViewModel : ViewModelBase
{
    private readonly NavigationService _navigation;
    public List<Course> Courses { get; }

    public CoursesViewModel(NavigationService navigation)
    { 
        _navigation = navigation;
        Courses = SystemManager.Courses;
    }
}