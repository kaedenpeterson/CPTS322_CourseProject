using Avalonia.Controls;
using ClassScheduler.CoreUI;
using ClassScheduler.Models;
using ClassScheduler.ViewModels;

namespace ClassScheduler.Views;

/// <summary>
/// Code-behind for StudentView.axaml.
/// </summary>
public partial class StudentView : UserControl
{
    public StudentView(INavigationService navigation, Student student)
    {
        InitializeComponent();
        DataContext = new StudentViewModel(navigation, student);
    }
}