/*
Description: UI for admin view (logged in) in ClassScheduler
Author: Bitna White 11812714
Date: 3-15-25
*/

using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media;
using ClassScheduler.CoreUI;
using ClassScheduler.ViewModels;

namespace ClassScheduler.Views;

public partial class CoursesView : UserControl
{
    public CoursesView(NavigationService navigation)
    {
        InitializeComponent();
        
        var seatsColorConverter = new FuncValueConverter<int, IBrush>(openSeats =>
            openSeats == 0 ? Brushes.Red : Brushes.LimeGreen
        );
        Resources["SeatsColorConverter"] = seatsColorConverter;
        
        DataContext = new CoursesViewModel(navigation);
    }
}