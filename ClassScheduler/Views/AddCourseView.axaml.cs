using Avalonia.Controls;
using ClassScheduler.CoreUI;
using ClassScheduler.Models;
using ClassScheduler.ViewModels;

namespace ClassScheduler.Views;

public partial class AddCourseView : UserControl
{
    public AddCourseView(INavigationService navigation)
    {
        InitializeComponent();
        DataContext = new AddCourseViewModel(navigation);
    }
}