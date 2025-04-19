using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Primitives;
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
    public CartViewModel(INavigationService navigation, Student student)
    { 
        _navigation = navigation;
        _student = student;
        SelectableCartCourses = _student.CartCourses.Select(course => new SelectableCourse(course)).ToList();

        foreach (var course in SelectableCartCourses)
        {
            course.RemoveCommand = new RelayCommand(() => Remove(course));
        }
    }

    private bool TryEnrollCourse(Course courseToEnroll)
    {
        // Check if there are open seats
        if (courseToEnroll.OpenSeats <= 0)
        {
            var noSeatsPopup = new PopupWindow(
                "No Seats",
                $"Cannot enroll in {courseToEnroll.Code} because there are no open seats.",
                "OK"
            );

            var window1 = (Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)
                ?.MainWindow;
            if (window1 != null) noSeatsPopup.ShowDialog(window1);
            
            return false;
        }
        
        // Check if student meets course prerequisites
        var pastCourseCodes = _student.PastCourses.Select(c => c.Code).ToHashSet();
        
        var missingPrereqs = courseToEnroll.Prerequisites
            .Where(prereq => !pastCourseCodes.Contains(prereq))
            .ToList();

        if (missingPrereqs.Any())
        {
            var missingList = string.Join(", ", missingPrereqs);

            var prereqPopup = new PopupWindow(
                "Missing Prerequisites",
                $"Cannot enroll in {courseToEnroll.Code}.\nMissing prerequisites: {missingList}",
                "OK"
            );
            
            var window2 = (Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)
                ?.MainWindow;
            if (window2 != null) prereqPopup.ShowDialog(window2);

            return false;
        }
        
        // Check time conflicts with enrolled courses
        var conflict = _student.EnrolledCourses.FirstOrDefault(enrolled =>
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

        var window3 = (Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)
            ?.MainWindow;
        if (window3 != null) conflictPopup.ShowDialog(window3);
        
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

            _student.CartCourses.Remove(course);
            _student.EnrolledCourses.Add(course);
        }
        
        OnPropertyChanged(nameof(SelectableCartCourses));
            
        _navigation.SwitchTo<CartView>(_student);
    }
    
    private void Remove(SelectableCourse selectableCourse)
    {
        if (selectableCourse == null) return;
        
        SelectableCartCourses.Remove(selectableCourse);
        _student.CartCourses.Remove(selectableCourse.Course);
        
        OnPropertyChanged(nameof(SelectableCartCourses));
        
        _navigation.SwitchTo<CartView>(_student);
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