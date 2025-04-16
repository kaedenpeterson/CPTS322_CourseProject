using System.Collections.Generic;
using System.Linq;
using ClassScheduler.CoreUI;
using ClassScheduler.Models;
using ClassScheduler.Views;
using CommunityToolkit.Mvvm.Input;

namespace ClassScheduler.ViewModels;

public partial class DropCoursesViewModel : ViewModelBase
{
    private readonly INavigationService _navigation;
    private readonly Student _student;

    public List<SelectableCourse> SelectableEnrolledCourses { get; }

    public DropCoursesViewModel(INavigationService navigation, Student student)
    { 
        _navigation = navigation;
        _student = student;
        SelectableEnrolledCourses = _student.EnrolledCourses.Select(course => new SelectableCourse(course)).ToList();
    }

    [RelayCommand]
    private void DropCourses()
    {
        var selectedCourses = SelectableEnrolledCourses.Where(c => c.IsSelected)
            .Select(c => c.Course)
            .ToList();

        foreach (var course in selectedCourses)
        {
            var selectableCourse = SelectableEnrolledCourses.FirstOrDefault(c => c.Course == course);
            if (selectableCourse != null)
            {
                SelectableEnrolledCourses.Remove(selectableCourse);
            }
            _student.EnrolledCourses.Remove(course);
            _navigation.SwitchTo<DropCoursesView>(_student);
            
            OnPropertyChanged(nameof(SelectableEnrolledCourses));
            OnPropertyChanged(nameof(_student.EnrolledCourses));
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