using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
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

    private bool TryEnrollCourse(Course courseToEnroll)
    {
        var conflict = EnrolledCourses.FirstOrDefault(enrolled =>
        {
            var conflictingDays = enrolled.Schedule.Days.Intersect(courseToEnroll.Schedule.Days);
            if (!conflictingDays.Any()) return false;

            return enrolled.Schedule.StartTime < courseToEnroll.Schedule.EndTime &&
                   courseToEnroll.Schedule.StartTime < enrolled.Schedule.EndTime;
        });

        if (conflict == null) return true;
        
        var conflictPopup = new PopupWindow(
            "Schedule Conflict",
            $"Cannot enroll in {courseToEnroll.Code} because it conflicts with {conflict.Code}.",
            "OK"
        );

        var parentWindow = (Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)
            ?.MainWindow;
        if (parentWindow != null) conflictPopup.ShowDialog(parentWindow);
        
        return false;
    }
    
    [RelayCommand]
    private void Enroll()
    {
        var selectedCourses = SelectableCartCourses.Where(c => c.IsSelected)
            .Select(c => c.Course)
            .ToList();

        foreach (var course in selectedCourses)
        {
            if (!TryEnrollCourse(course)) continue;

            var selectableCourse = SelectableCartCourses.FirstOrDefault(c => c.Course == course);
            if (selectableCourse != null) SelectableCartCourses.Remove(selectableCourse);

            CartCourses.Remove(course);
            EnrolledCourses.Add(course);
        }
        
        OnPropertyChanged(nameof(SelectableCartCourses));
        OnPropertyChanged(nameof(CartCourses));
        OnPropertyChanged(nameof(EnrolledCourses));
            
        _navigation.SwitchTo<CartView>(_student);
    }
    
    private void Remove(SelectableCourse selectableCourse)
    {
        if (selectableCourse == null) return;
        
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