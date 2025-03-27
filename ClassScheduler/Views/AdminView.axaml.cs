/*
Description: Code-behind for the admin view (logged in) in ClassScheduler.
Author: Kaeden Peterson 11858249
Date: 3-15-25
*/

using Avalonia.Controls;
using ClassScheduler.CoreUI;
using ClassScheduler.Models;
using ClassScheduler.ViewModels;

namespace ClassScheduler.Views;

public partial class AdminView : UserControl
{
    public AdminView(INavigationService navigation, Admin admin)
    {
        InitializeComponent();
        DataContext = new AdminViewModel(navigation, admin);
    }
}