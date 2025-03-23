using Avalonia.Controls;
using Avalonia.Interactivity;

namespace ClassScheduler.CoreUI.Student;

public partial class StudentRootView : UserControl
{
    public StudentRootView(NavigationService navigation, Models.Student student)
    {
        InitializeComponent();
        DataContext = new StudentRootViewModel(navigation, student);
    }
}