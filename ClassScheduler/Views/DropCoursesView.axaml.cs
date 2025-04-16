using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.VisualTree;
using ClassScheduler.CoreUI;
using ClassScheduler.Models;
using ClassScheduler.ViewModels;

namespace ClassScheduler.Views;

public partial class DropCoursesView : UserControl
{
    public DropCoursesView(INavigationService navigation, Student student)
    {
        InitializeComponent();
        
        DataContext = new DropCoursesViewModel(navigation, student);
    }

    private void OnDropCoursesSelect(object sender, RoutedEventArgs e)
    {
        if (DataContext is not DropCoursesViewModel vm) return;

        var confirmDropCourses = new PopupWindow(
            "Confirm Drop",
            "Are you sure you want to drop the selected courses?",
            "Yes", () => vm.DropCoursesCommand.Execute(null),
            "Cancel"
        );
        
        var parentWindow = this.FindAncestorOfType<Window>();
        if (parentWindow != null) confirmDropCourses.ShowDialog(parentWindow);
    }
}