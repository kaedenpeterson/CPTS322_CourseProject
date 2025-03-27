using Avalonia.Controls;
using ClassScheduler.CoreUI;
using ClassScheduler.Models;
using ClassScheduler.ViewModels;

namespace ClassScheduler.Views;

/// <summary>
/// Code-behind for AdminView.axaml.
/// </summary>
public partial class AdminView : UserControl
{
    public AdminView(INavigationService navigation, Admin admin)
    {
        InitializeComponent();
        DataContext = new AdminViewModel(navigation, admin);
    }
}