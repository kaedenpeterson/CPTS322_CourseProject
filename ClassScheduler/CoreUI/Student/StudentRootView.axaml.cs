using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.VisualTree;
using ClassScheduler.Views;

namespace ClassScheduler.CoreUI.Student;

/// <summary>
/// Code-behind for StudentRootView.axaml.
/// </summary>
public partial class StudentRootView : UserControl
{
    public StudentRootView(INavigationService navigation, Models.Student student)
    {
        InitializeComponent();
        DataContext = new StudentRootViewModel(navigation, student);
    }                                                             

    private void OnLogoutSelect(object sender, PointerPressedEventArgs e)
    {
        if (DataContext is not StudentRootViewModel vm) return;
        
        var confirmLogoutPopup = new PopupWindow(
            string.Empty,
            "Are you sure you want to log out?",
            "Yes", () => vm.Navigation.SwitchTo<LoginView>(),
            "Cancel"
        );
        
        var parentWindow = this.FindAncestorOfType<Window>();
        if (parentWindow != null) confirmLogoutPopup.ShowDialog(parentWindow);
    }
}