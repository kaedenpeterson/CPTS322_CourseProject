using System.Collections.Generic;
using ClassScheduler.CoreUI;
using ClassScheduler.Models;
using ClassScheduler.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ClassScheduler.ViewModels;

public partial class EditCourseViewModel : ViewModelBase
{
    private readonly INavigationService _navigation;

    [ObservableProperty] private string name;
    [ObservableProperty] private string code;
    [ObservableProperty] private string instructor;
    [ObservableProperty] private string description;
    [ObservableProperty] private int credits;
    [ObservableProperty] private string location;
    [ObservableProperty] private string status;
    
    public List<int> CreditOptions { get; } = [1, 2, 3, 4];
    public List<string> StatusOptions { get; } = ["Active", "Inactive"];

    private readonly Course _originalCourse;

    public EditCourseViewModel(INavigationService navigation, Course course)
    {
        _navigation = navigation;
        _originalCourse = course;
        
        Name = course.Name;
        Code = course.Code;
        Instructor = course.Instructor;
        Description = course.Description;
        Credits = course.Credits;
        Location = course.Location;
        Status = course.IsActive ? "Active" : "Inactive";
    }

    [RelayCommand]
    private void Save()
    {
        _originalCourse.Name = Name;
        _originalCourse.Code = Code;
        _originalCourse.Instructor = Instructor;
        _originalCourse.Description = Description;
        _originalCourse.Credits = Credits;
        _originalCourse.Location = Location;
        _originalCourse.IsActive = Status == "Active";

        _navigation.SwitchTo<AdminCoursesView>();
    }

    [RelayCommand]
    private void Cancel()
    {
        _navigation.SwitchTo<AdminCoursesView>();
    }
}