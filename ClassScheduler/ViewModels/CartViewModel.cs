using System.Collections.Generic;
using System.Linq;
using ClassScheduler.CoreUI;
using ClassScheduler.Models;
using CommunityToolkit.Mvvm.Input;

namespace ClassScheduler.ViewModels;

public partial class CartViewModel : ViewModelBase
{
    private readonly INavigationService _navigation;
    private readonly Student _student;

    public List<SelectableCourse> SelectableCartCourses { get; }
    public List<Course> CartCourses { get; }
    public List<Course> EnrolledCourses { get; }

    public CartViewModel(INavigationService navigation, Student student)
    { 
        _navigation = navigation;
        _student = student;
        SelectableCartCourses = _student.Cart.Select(course => new SelectableCourse(course)).ToList();
        CartCourses = _student.Cart;
        EnrolledCourses = _student.Courses;
    }

    [RelayCommand]
    private void Enroll()
    {
        var selectedCourses = SelectableCartCourses.Where(c => c.IsSelected)
            .Select(c => c.Course)
            .ToList();

        foreach (var course in selectedCourses)
        {
            var selectableCourse = SelectableCartCourses.FirstOrDefault(c => c.Course == course);
            if (selectableCourse != null)
            {
                SelectableCartCourses.Remove(selectableCourse);
            }
            CartCourses.Remove(course);
            EnrolledCourses.Add(course);
            
            OnPropertyChanged(nameof(SelectableCartCourses));
            OnPropertyChanged(nameof(CartCourses));
            OnPropertyChanged(nameof(EnrolledCourses));
        }
    }

    public class SelectableCourse : ViewModelBase
    {
        public Course Course { get; set; }
        private bool _isSelected;

        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value); 
        }

        public SelectableCourse(Course course)
        {
            Course = course;
        }
        
    }
}