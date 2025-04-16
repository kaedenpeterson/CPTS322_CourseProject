using System;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media;
using ClassScheduler.CoreUI;
using ClassScheduler.ViewModels;
using System.Windows.Input;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using ClassScheduler.Data;
using ClassScheduler.Models;

namespace ClassScheduler.Views;

/// <summary>
/// Code-behind for AdminCoursesView.axaml.
/// </summary>
public partial class AdminCoursesView : UserControl
{
    public AdminCoursesView(INavigationService navigation)
    {
        InitializeComponent();

        DataContext = new AdminCoursesViewModel(navigation);

        var seatsColorConverter = new FuncValueConverter<int, IBrush>(openSeats =>
            openSeats == 0 ? Brushes.Red : Brushes.LimeGreen);
        
        var statusTextConverter = new FuncValueConverter<bool, string>(isActive =>
            isActive ? "Active" : "Inactive");
        
        var statusColorConverter = new FuncValueConverter<bool, IBrush>(isActive =>
            isActive ? Brushes.LimeGreen : Brushes.Red);

        Resources["SeatsColorConverter"] = seatsColorConverter;
        Resources["StatusTextConverter"] = statusTextConverter;
        Resources["StatusColorConverter"] = statusColorConverter;
    }

    private void OnDeleteCourseSelect(object sender, RoutedEventArgs e)
    {
        if (DataContext is not AdminCoursesViewModel vm) return;
        if (sender is not Button { Tag: Course courseToDelete }) return;

        var confirmDeleteCourse = new PopupWindow(
            "Confirm Deletion",
            $"Are you sure you want to delete {courseToDelete.Code}?",
            "Yes", () => vm.DeleteCourse(courseToDelete),
            "Cancel"
        );

        var parentWindow = this.FindAncestorOfType<Window>();
        if (parentWindow != null) confirmDeleteCourse.ShowDialog(parentWindow);
    }
}
