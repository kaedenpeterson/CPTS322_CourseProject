using System.Linq;
using Avalonia;
using Avalonia.Controls;
using ClassScheduler.CoreUI;
using ClassScheduler.ViewModels;

namespace ClassScheduler.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel(new NavigationService());
    }
}