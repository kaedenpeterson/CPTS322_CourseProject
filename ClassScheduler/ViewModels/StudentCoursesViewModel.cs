using System.Collections.Generic;
using System.Linq;
using ClassScheduler.CoreUI;
using ClassScheduler.Data;
using ClassScheduler.Models;
using CommunityToolkit.Mvvm.Input;

namespace ClassScheduler.ViewModels;

/// <summary>
/// ViewModel for the Courses page, responsible for managing the list of courses displayed for the student.
/// Shown as a sub view (CurrView) in the window.
/// </summary>
public partial class StudentCoursesViewModel : ViewModelBase
{
    private readonly INavigationService _navigation;
    private readonly Student _student;
    public List<Course> Courses { get; }

    public StudentCoursesViewModel(INavigationService navigation, Student student)
    { 
        _navigation = navigation;
        _student = student;
        
        // Only display courses that are set as active
        Courses = SystemManager.Courses
            .Where(course => course.IsActive)
            .ToList();
        
        foreach (var course in Courses)
        {
            course.AddToCartCommand = new RelayCommand(() => AddToCart(course));
        }
    }
    
    private void AddToCart(Course course)
    {
        _student.CartCourses.Add(course);
        course.IsInCart = true;
    }
}