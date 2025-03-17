using ClassScheduler.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace ClassScheduler.ViewModels;

public partial class CoursesViewModel : ViewModelBase
{
    public ObservableCollection<string> Courses { get; }

    public CoursesViewModel()
    {
        Courses = new ObservableCollection<string>
        {
            "Test Course"
        };
    }
}