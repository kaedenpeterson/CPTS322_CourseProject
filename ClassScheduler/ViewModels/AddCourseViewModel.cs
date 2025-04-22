using System.Collections.Generic;
using ClassScheduler.CoreUI;
using ClassScheduler.Models;
using ClassScheduler.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ClassScheduler.Data;
using System;

namespace ClassScheduler.ViewModels;

public partial class AddCourseViewModel : ViewModelBase
{
    private readonly INavigationService _navigation;

    [ObservableProperty] private string name;
    [ObservableProperty] private string code;
    [ObservableProperty] private string instructor;
    [ObservableProperty] private string description;
    [ObservableProperty] private int credits;
    [ObservableProperty] private string location;
    [ObservableProperty] private string status;
    [ObservableProperty] private string _errorMessage = string.Empty;
    [ObservableProperty] private int maxSeats;



    public List<int> CreditOptions { get; } = [1, 2, 3, 4];
    public List<string> StatusOptions { get; } = ["Active", "Inactive"];

    private readonly Course _originalCourse;

    public AddCourseViewModel(INavigationService navigation)
    {
        _navigation = navigation;
    }


    [RelayCommand]
    private void Save()
    {
        var newCourse = new Course(
             Code,
             Name,
             Instructor,
             Description,
             Credits,
             new List<string>(), // Empty prereqs for now
             30, // Default max seats
             Location,
             Status == "Active",
             new Schedule(
                 new List<DayOfWeek>(),                         // You can replace this with actual selected days later
                 new TimeSpan(9, 0, 0),                         // Start time
                 new TimeSpan(10, 0, 0),                        // End time
                 DateTime.Today,                                // Start date
                 DateTime.Today.AddMonths(3)                    // End date
             )
        );
        if (string.IsNullOrEmpty(newCourse.Code) || string.IsNullOrEmpty(newCourse.Name) || string.IsNullOrEmpty(newCourse.Instructor) || string.IsNullOrEmpty(newCourse.Description) || newCourse.Credits == null || string.IsNullOrEmpty(newCourse.Location))
        {
            ErrorMessage = "A field is missing credentials";
            return;
        }


        SystemManager.Courses.Add(newCourse);
        _navigation.SwitchTo<AdminCoursesView>();
    }


    [RelayCommand]
    private void Cancel()
    {
        _navigation.SwitchTo<AdminCoursesView>();
    }
}