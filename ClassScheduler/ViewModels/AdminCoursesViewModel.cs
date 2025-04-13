using System.Collections.Generic;
using ClassScheduler.CoreUI;
using ClassScheduler.Data;
using ClassScheduler.Models;
using ClassScheduler.Views;
using CommunityToolkit.Mvvm.Input;

namespace ClassScheduler.ViewModels;

/// <summary>
/// ViewModel for the Admin Courses page, responsible for managing the list of courses displayed for
/// users logged in as Admin.
/// </summary>
public partial class AdminCoursesViewModel : ViewModelBase
{
    private readonly INavigationService _navigation;
    public List<Course> Courses { get; }

    public AdminCoursesViewModel(INavigationService navigation)
    {
        _navigation = navigation;
        Courses = SystemManager.Courses;
        
        foreach (var course in Courses)
        {
            course.EditCourseCommand = new RelayCommand(() => EditCourse(course));
        }
    }
    
    private void EditCourse(Course course)
    {
        _navigation.SwitchTo<EditCourseView>(course);
    }
}