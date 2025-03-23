using System.Collections.ObjectModel;
using ClassScheduler.CoreUI;

namespace ClassScheduler.ViewModels;

public partial class CoursesViewModel : ViewModelBase
{
    public ObservableCollection<string> Courses { get; }
    
    private readonly NavigationService _navigation;

    public CoursesViewModel(NavigationService navigation)
    {
        _navigation = navigation;
        Courses = new ObservableCollection<string>
        {
            "Test Course"
        };
    }
}