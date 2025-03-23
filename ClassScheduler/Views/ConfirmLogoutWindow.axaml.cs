using Avalonia.Controls;
using Avalonia.Interactivity;
using ClassScheduler.CoreUI;
using ClassScheduler.CoreUI.Student;

namespace ClassScheduler.Views;

public partial class ConfirmLogoutWindow : Window
{
    private readonly INavigationService _navigation;

    public ConfirmLogoutWindow(INavigationService navigation)
    {
        InitializeComponent();
        _navigation = navigation;
        WindowStartupLocation = WindowStartupLocation.CenterOwner;
    }
        
    private void OnCancelButtonSelect(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void OnYesButtonSelect(object sender, RoutedEventArgs e)
    {
        Close();
       _navigation.NavigateTo<LoginView>();
    }
}