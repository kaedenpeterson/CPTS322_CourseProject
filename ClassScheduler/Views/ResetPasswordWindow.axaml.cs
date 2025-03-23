using Avalonia.Controls;
using Avalonia.Interactivity;

namespace ClassScheduler.Views;

public partial class ResetPasswordWindow : Window
{
    public ResetPasswordWindow()
    {
        InitializeComponent();
        WindowStartupLocation = WindowStartupLocation.CenterOwner;
    }
        
    private void OnOkButtonSelect(object sender, RoutedEventArgs e)
    {
        Close();
    }
}