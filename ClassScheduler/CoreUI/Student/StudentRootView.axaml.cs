using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using ClassScheduler.Views;

namespace ClassScheduler.CoreUI.Student;

public partial class StudentRootView : UserControl
{
    public StudentRootView(NavigationService navigation, Models.Student student)
    {
        InitializeComponent();
        DataContext = new StudentRootViewModel(navigation, student);
    }
    
    private void OnLogoutSelect(object sender, PointerPressedEventArgs e)
    {
        if (DataContext is StudentRootViewModel vm)
        {
            var confirmLogoutWindow = new ConfirmLogoutWindow(vm.Navigation);
            var parentWindow = this.FindAncestorOfType<Window>();
            if (parentWindow != null)
            {
                confirmLogoutWindow.ShowDialog(parentWindow);
            }
        }
    }
}