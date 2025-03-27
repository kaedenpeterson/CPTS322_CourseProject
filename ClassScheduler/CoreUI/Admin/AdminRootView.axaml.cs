using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.VisualTree;
using ClassScheduler.Views;

namespace ClassScheduler.CoreUI.Admin;

public partial class AdminRootView : UserControl
{
    public AdminRootView(INavigationService navigation, Models.Admin admin)
    {
        InitializeComponent();
        DataContext = new AdminRootViewModel(navigation, admin);
    }                                                             
    
    private void OnLogoutSelect(object sender, PointerPressedEventArgs e)
    {
        if (DataContext is not AdminRootViewModel vm) return;
        
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