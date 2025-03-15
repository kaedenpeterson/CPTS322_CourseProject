/*
Description: Code-behind for login page in ClassScheduler
Author: Kaeden Peterson 11858249
Date: 3-14-25
*/

using Avalonia.Controls;
using Avalonia.Input;
using ClassScheduler.ViewModels;

namespace ClassScheduler.Views;

public partial class LoginView : Window
{
    public LoginView()
    {
       InitializeComponent();
       Loaded += (_, _) =>
       {
           if (DataContext is LoginViewModel vm)
           {
               vm.LoginSuccess += Close;
           }
       };
    }

    private void OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter && DataContext is LoginViewModel vm)
        {
            vm.LoginCommand.Execute(null);
        }
    }
}