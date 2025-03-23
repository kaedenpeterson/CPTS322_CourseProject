using System.Linq;
using Avalonia.Controls;
using ClassScheduler.ViewModels;
using ClassScheduler.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ClassScheduler.CoreUI.Student;

public partial class StudentRootViewModel : ViewModelBase
{
    public INavigationService Navigation { get; }

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
            if (args.PropertyName == nameof(NavigationService.CurrView))
            {
                CurrView = Navigation.CurrView;
            }
        };
    }
    
    public string? FirstName => _student.Name.Split(' ').FirstOrDefault();
    
    [RelayCommand]
    private void NavigateToHome()
    {
        Navigation.NavigateTo<StudentView>(_student);
    }

    [RelayCommand]
    private void NavigateToCourses()
    {
        Navigation.NavigateTo<CoursesView>();
    }
}