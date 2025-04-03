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
    public readonly INavigationService Navigation;

    private readonly Models.Student _student;
    
    [ObservableProperty]
    private UserControl _currView;
    
    public StudentRootViewModel(INavigationService navigation, Models.Student student)
    {
        Navigation = navigation;
        _student = student;
        
        _currView = Navigation.CurrView;
        
        Navigation.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(NavigationService.CurrView)) CurrView = Navigation.CurrView;
        };
    }
    
    public string? FirstName => _student.Name.Split(' ').FirstOrDefault();
    
    [RelayCommand]
    private void NavigateToHome()
    {
        Navigation.SwitchTo<StudentView>(_student);
    }

    [RelayCommand]
    private void NavigateToCourses()
    {
        Navigation.SwitchTo<StudentCoursesView>();
    }
}