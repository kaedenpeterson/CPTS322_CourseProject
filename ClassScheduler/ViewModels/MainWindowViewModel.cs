using Avalonia.Controls;
using ClassScheduler.CoreUI;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ClassScheduler.ViewModels;

/// <summary>
/// ViewModel for the main window of the application.
/// </summary>
public partial class MainWindowViewModel : ViewModelBase
{
    private readonly INavigationService _navigation;
    
    [ObservableProperty] private UserControl _mainView;

    public MainWindowViewModel(INavigationService navigation)
    {
        _navigation = navigation;
        _mainView = _navigation.MainView;

        _navigation.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(INavigationService.MainView))
            {
                MainView = _navigation.MainView;
            }
        };
    }
}