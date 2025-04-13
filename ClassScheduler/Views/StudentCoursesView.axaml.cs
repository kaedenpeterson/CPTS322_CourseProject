using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media;
using ClassScheduler.CoreUI;
using ClassScheduler.Models;
using ClassScheduler.ViewModels;

namespace ClassScheduler.Views;

/// <summary>
/// Code-behind for StudentCoursesView.axaml.
/// </summary>
public partial class StudentCoursesView : UserControl
{
    public StudentCoursesView(INavigationService navigation, Student student)
    {
        InitializeComponent();
        
        var seatsColorConverter = new FuncValueConverter<int, IBrush>(openSeats =>
            openSeats == 0 ? Brushes.Red : Brushes.LimeGreen);
        
        /*
        var statusColorConverter = new FuncValueConverter<bool, IBrush>(isActive =>
            isActive ? Brushes.LimeGreen : Brushes.Red);
            
        */
        
        Resources["SeatsColorConverter"] = seatsColorConverter;
        // Resources["StatusColorConverter"] = statusColorConverter;
        
        DataContext = new StudentCoursesViewModel(navigation, student);
    }
}