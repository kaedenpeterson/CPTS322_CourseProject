using Avalonia.Controls;
using ClassScheduler.CoreUI;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ClassScheduler.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly NavigationService _navigation;
    
    [ObservableProperty] private UserControl _mainView;

    public MainWindowViewModel(NavigationService navigation)
    {
        _navigation = navigation;
        _mainView = _navigation.MainView;

        _navigation.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(NavigationService.MainView))
            {
                MainView = _navigation.MainView;
            }
        };
    }
}