/*
Description: Code-behind for the admin view (logged in) in ClassScheduler.
Author: Kaeden Peterson 11858249
Date: 3-15-25
*/

using Avalonia.Controls;
using ClassScheduler.Models;
using ClassScheduler.ViewModels;

namespace ClassScheduler.Views;

public partial class AdminView : UserControl
{
    public AdminView(Admin admin, ViewManager navigation)
    {
        InitializeComponent();
        DataContext = new AdminViewModel(admin, navigation);
    }
}