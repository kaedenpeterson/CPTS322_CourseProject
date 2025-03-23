/*
Description: Code-behind for login page in ClassScheduler
Author: Kaeden Peterson 11858249
Date: 3-14-25
*/

using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.LogicalTree;
using Avalonia.Media;
using Avalonia.VisualTree;
using ClassScheduler.CoreUI;
using ClassScheduler.ViewModels;

namespace ClassScheduler.Views;

public partial class LoginView : UserControl
{
    private readonly NavigationService _navigation;
    public LoginView(NavigationService navigation)
    {
       InitializeComponent();
       DataContext = new LoginViewModel(navigation);
    }

    private void OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter && DataContext is LoginViewModel vm)
        {
            vm.LoginCommand.Execute(null);
        }
    }
    
    private void OnPointerEnter(object sender, PointerEventArgs e)
    {
        if (sender is TextBlock textBlock)
        {
            textBlock.Foreground = Brushes.Gray;
        }
    }

    private void OnPointerExit(object sender, PointerEventArgs e)
    {
        if (sender is TextBlock textBlock)
        {
            textBlock.Foreground = Brushes.DodgerBlue;
        }
    }
    
    private void OnForgotPasswordClick(object sender, PointerPressedEventArgs e)
    {
        var resetPasswordWindow = new ResetPasswordWindow();
        var parentWindow = this.FindAncestorOfType<Window>();
        if (parentWindow != null)
        {
            resetPasswordWindow.ShowDialog(parentWindow); 
        }
    }
}