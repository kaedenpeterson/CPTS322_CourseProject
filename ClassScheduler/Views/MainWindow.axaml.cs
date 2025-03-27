using System.Linq;
using Avalonia;
using Avalonia.Controls;
using ClassScheduler.CoreUI;
using ClassScheduler.ViewModels;

namespace ClassScheduler.Views;

/// <summary>
/// Code-behind for MainWindow.axaml.
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel(new NavigationService());
    }
}