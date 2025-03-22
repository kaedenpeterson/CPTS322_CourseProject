/*
Description: ViewModel for the student view (logged in) in ClassScheduler.
Author: Kaeden Peterson 11858249, Bitna White 11812714
Date: 3-15-25
*/

using System.Collections.ObjectModel;
using ClassScheduler.Models;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using ClassScheduler.Views;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ClassScheduler.ViewModels;

public partial class StudentViewModel : ViewModelBase
{
    [ObservableProperty] private Student _student;
    [ObservableProperty] private ObservableCollection<Course> _courses;
    
    private readonly ViewManager _navigation;
    
    public StudentViewModel(Student student, ViewManager navigation)
    {
        Student = student;
        Courses = new ObservableCollection<Course>(student.Courses);
        
        _navigation = navigation;
        
    }
    
    [RelayCommand]
    private void BrowseCourses()
    {
        // ViewManager.ChangeView(new CoursesView { DataContext = new CoursesViewModel(ViewManager) });
    }

    // public ICommand CourseCommand { get; }
    
}