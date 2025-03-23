/*
Description: ViewModel for the student view (logged in) in ClassScheduler.
Author: Kaeden Peterson 11858249, Bitna White 11812714
Date: 3-15-25
*/

using System.Collections.ObjectModel;
using ClassScheduler.Models;
using ClassScheduler.CoreUI;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ClassScheduler.ViewModels;

public partial class StudentViewModel : ViewModelBase
{
    [ObservableProperty] private Student _student;
    [ObservableProperty] private ObservableCollection<Course> _courses;
    
    private readonly NavigationService _navigation;

    public StudentViewModel(Student student, NavigationService navigation)
    {
        Student = student;
        Courses = new ObservableCollection<Course>(student.Courses);

        _navigation = navigation;
        
    }
}