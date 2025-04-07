using Avalonia.Controls;
using ClassScheduler.CoreUI;
using ClassScheduler.Models;
using ClassScheduler.ViewModels;

namespace ClassScheduler.Views;

public partial class EditCourseView : UserControl
{
    public EditCourseView(INavigationService navigation, Course course)
    {
        InitializeComponent();
        DataContext = new EditCourseViewModel(navigation, course);
    }
}