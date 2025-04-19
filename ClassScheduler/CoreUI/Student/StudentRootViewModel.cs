using System.Linq;
using Avalonia.Controls;
using ClassScheduler.ViewModels;
using ClassScheduler.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ClassScheduler.CoreUI.Student;

/// <summary>
/// ViewModel for the student root view, responsible for handling navigation through a menu bar.
/// Also displays the sub view (CurrView) as a ContentControl.
/// </summary>
public partial class StudentRootViewModel : ViewModelBase
{
    public readonly INavigationService _navigation;

    private readonly Models.Student _student;
    
    [ObservableProperty]
    private UserControl _currView;
    
    public StudentRootViewModel(INavigationService navigation, Models.Student student)
    {
        _navigation = navigation;
        _student = student;
        
        _currView = _navigation.CurrView;
        
        _navigation.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(NavigationService.CurrView)) CurrView = _navigation.CurrView;
        };
    }
    
    public string? FirstName => _student.Name.Split(' ').FirstOrDefault();
    
    [RelayCommand]
    private void NavigateToHome()
    {
        _navigation.SwitchTo<StudentView>(_student);
    }

    [RelayCommand]
    private void NavigateToCourses()
    {
        _navigation.SwitchTo<StudentCoursesView>(_student);
    }

    [RelayCommand]
    private void NavigateToCart()
    {
        _navigation.SwitchTo<CartView>(_student);
    }

    [RelayCommand]
    private void NavigateToDropCourses()
    {
        _navigation.SwitchTo<DropCoursesView>(_student);
    }
}