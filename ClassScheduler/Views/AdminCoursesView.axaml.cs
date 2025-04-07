using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media;
using ClassScheduler.CoreUI;
using ClassScheduler.ViewModels;
using System.Windows.Input;

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

        /*
        var statusColorConverter = new FuncValueConverter<bool, IBrush>(isActive =>
            isActive ? Brushes.LimeGreen : Brushes.Red);
            
        */

        Resources["SeatsColorConverter"] = seatsColorConverter;
        // Resources["StatusColorConverter"] = statusColorConverter;
    }
}
