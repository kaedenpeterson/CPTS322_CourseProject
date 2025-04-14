using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using ClassScheduler.CoreUI;
using ClassScheduler.Models;
using ClassScheduler.Views;
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
        SelectableCartCourses = _student.CartCourses.Select(course => new SelectableCourse(course)).ToList();
        CartCourses = _student.CartCourses;
        EnrolledCourses = _student.Courses;
        
        foreach (var course in SelectableCartCourses)
        {
            course.RemoveCommand = new RelayCommand(() => Remove(course));
        }
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
            _navigation.SwitchTo<CartView>(_student);
            
            OnPropertyChanged(nameof(SelectableCartCourses));
            OnPropertyChanged(nameof(CartCourses));
            OnPropertyChanged(nameof(EnrolledCourses));
        }
    }
    
    private void Remove(SelectableCourse selectableCourse)
    {
        if (selectableCourse is null) return;
        
        SelectableCartCourses.Remove(selectableCourse);
        CartCourses.Remove(selectableCourse.Course);
        _navigation.SwitchTo<CartView>(_student);
        
        OnPropertyChanged(nameof(SelectableCartCourses));
        OnPropertyChanged(nameof(CartCourses));
    }

    public class SelectableCourse : ViewModelBase
    {
        public Course Course { get; set; }
        private bool _isSelected;
        
        public ICommand RemoveCommand {get; set;}

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