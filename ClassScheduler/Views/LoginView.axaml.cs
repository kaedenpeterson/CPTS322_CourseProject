/*
Description: Code-behind for login page in ClassScheduler
Author: Kaeden Peterson 11858249
Date: 3-14-25
*/

using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.VisualTree;
using ClassScheduler.CoreUI;
using ClassScheduler.ViewModels;

namespace ClassScheduler.Views;

/// <summary>
/// Code-behind for LoginView.axaml.
/// </summary>
public partial class LoginView : UserControl
{
    public LoginView(INavigationService navigation)
    {
       InitializeComponent();
       DataContext = new LoginViewModel(navigation);
    }

    private void OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter && DataContext is LoginViewModel vm) vm.LoginCommand.Execute(null);
    }
    
    private void OnPointerEnter(object sender, PointerEventArgs e)
    {
        if (sender is TextBlock textBlock) textBlock.Foreground = Brushes.Gray;
    }

    private void OnPointerExit(object sender, PointerEventArgs e)
    {
        if (sender is TextBlock textBlock) textBlock.Foreground = Brushes.DodgerBlue;
    }
    
    private void OnForgotPasswordSelect(object sender, PointerPressedEventArgs e)
    {
        if (DataContext is not LoginViewModel vm) return;

        var forgotPasswordPopup = new PopupWindow(
            string.Empty,
            "Too bad",
            "OK"
        );
        
        var parentWindow = this.FindAncestorOfType<Window>();
        if (parentWindow != null) forgotPasswordPopup.ShowDialog(parentWindow);
    }
}