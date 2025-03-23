using System.Linq;
using Avalonia.Controls;
using ClassScheduler.ViewModels;
using ClassScheduler.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ClassScheduler.CoreUI.Student;

public partial class StudentRootViewModel : ViewModelBase
{
    private readonly INavigationService _navigation;
    private readonly Models.Student _student;
    
    [ObservableProperty]
    private UserControl _currView;
    
    public StudentRootViewModel(INavigationService navigation, Models.Student student)
    {
        _navigation = navigation;
        _student = student;
        
        _currView = navigation.CurrView;
        
        _navigation.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(NavigationService.CurrView))
            {
                CurrView = _navigation.CurrView;
            }
        };
    }
    
    public string? FirstName => _student.Name.Split(' ').FirstOrDefault();
    
    [RelayCommand]
    private void NavigateToHome()
    {
        _navigation.NavigateTo<StudentView>(_student);
    }

    [RelayCommand]
    private void NavigateToLogin()
    {
        _navigation.NavigateTo<LoginView>();
    }

    [RelayCommand]
    private void NavigateToCourses()
    {
        _navigation.NavigateTo<CoursesView>();
    }
}