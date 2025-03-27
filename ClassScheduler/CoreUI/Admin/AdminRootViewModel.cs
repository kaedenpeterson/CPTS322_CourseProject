using System.Linq;
using Avalonia.Controls;
using ClassScheduler.ViewModels;
using ClassScheduler.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ClassScheduler.CoreUI.Admin;

public partial class AdminRootViewModel : ViewModelBase
{
    public readonly INavigationService Navigation;

    private readonly Models.Admin _admin;
    
    [ObservableProperty]
    private UserControl _currView;
    
    public AdminRootViewModel(INavigationService navigation, Models.Admin admin)
    {
        Navigation = navigation;
        _admin = admin;
        
        _currView = Navigation.CurrView;
        
        Navigation.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(NavigationService.CurrView)) CurrView = Navigation.CurrView;
        };
    }
    
    public string? FirstName => _admin.Name.Split(' ').FirstOrDefault();
    
    [RelayCommand]
    private void NavigateToHome()
    {
        Navigation.SwitchTo<AdminView>(_admin);
    }

    [RelayCommand]
    private void NavigateToCourses()
    {
        Navigation.SwitchTo<CoursesView>();
    }
}