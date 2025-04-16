using System.Collections.ObjectModel;
using ClassScheduler.Models;
using ClassScheduler.CoreUI;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ClassScheduler.ViewModels;

/// <summary>
/// ViewModel for the student homepage. Shown as a sub view (CurrView) in the window.
/// </summary>
public partial class StudentViewModel : ViewModelBase
{
    [ObservableProperty] private Student _student;
    [ObservableProperty] private ObservableCollection<Course> _courses;
    
    private readonly INavigationService _navigation;

    public StudentViewModel(INavigationService navigation, Student student)
    {
        Student = student;
        Courses = new ObservableCollection<Course>(student.EnrolledCourses);

        _navigation = navigation;
        
    }
}