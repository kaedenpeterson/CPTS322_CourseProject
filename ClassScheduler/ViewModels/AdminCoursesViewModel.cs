using System;
using System.Collections.Generic;
using System.Windows.Input;
using ClassScheduler.CoreUI;
using ClassScheduler.CoreUI.Admin;
using ClassScheduler.CoreUI.Student;
using ClassScheduler.Data;
using ClassScheduler.Models;
using ClassScheduler.Views;
using CommunityToolkit.Mvvm.ComponentModel;
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
    }

    [RelayCommand]
    private void EditCourse(Course course)
    {
        var fetchedCourse = SystemManager.GetCourse(course.Name);
        if (fetchedCourse is not null) _navigation.SwitchTo<EditCourseView>(fetchedCourse);
    }
}