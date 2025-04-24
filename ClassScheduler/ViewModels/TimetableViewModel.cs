using ClassScheduler.CoreUI;
using ClassScheduler.Models;
using ClassScheduler.ViewModels;
using System.Collections.ObjectModel;

namespace ClassScheduler.ViewModels;

public partial class TimetableViewModel : ViewModelBase
{
    private readonly INavigationService _navigation;
    public Student Student { get; }

    public ObservableCollection<Course> EnrolledCourses { get; } = new();

    public TimetableViewModel(INavigationService navigation, Student student)
    {
        _navigation = navigation;
        Student = student;

        foreach (var course in student.EnrolledCourses)
        {
            EnrolledCourses.Add(course);
        }
    }
}