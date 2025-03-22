using System.Collections.ObjectModel;

namespace ClassScheduler.ViewModels;

public partial class CoursesViewModel : ViewModelBase
{
    public ObservableCollection<string> Courses { get; }
    
    private readonly ViewManager _navigation;

    public CoursesViewModel(ViewManager navigation)
    {
        _navigation = navigation;
        Courses = new ObservableCollection<string>
        {
            "Test Course"
        };
    }
}