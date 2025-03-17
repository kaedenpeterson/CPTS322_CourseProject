/*
Description: ViewModel for the student view (logged in) in ClassScheduler.
Author: Kaeden Peterson 11858249 && Bitna White 11812714
Date: 3-15-25
*/

using ClassScheduler.Models;
using ClassScheduler.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace ClassScheduler.ViewModels;

public partial class StudentViewModel : ViewModelBase
{
    private Student _student;

    public StudentViewModel(Student student)
    {
        _student = student;
        CourseCommand = new RelayCommand(ClickViewCourse);
    }

    public ICommand CourseCommand { get; }

    private void ClickViewCourse()
    {
        var coursesView = new CoursesView();
        coursesView.Show();
    }
}