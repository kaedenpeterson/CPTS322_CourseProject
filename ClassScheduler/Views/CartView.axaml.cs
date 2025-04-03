using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media;
using ClassScheduler.CoreUI;
using ClassScheduler.Models;
using ClassScheduler.ViewModels;

namespace ClassScheduler.Views;

public partial class CartView : UserControl
{
    public CartView(INavigationService navigation, Student student)
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
        
        DataContext = new CartViewModel(navigation, student);
    }
}