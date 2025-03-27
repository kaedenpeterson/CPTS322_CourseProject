/*
Description: ViewModel for the admin view (logged in) in ClassScheduler.
Author: Kaeden Peterson 11858249
Date: 3-15-25
*/

using ClassScheduler.CoreUI;
using ClassScheduler.Models;

namespace ClassScheduler.ViewModels;

public partial class AdminViewModel : ViewModelBase
{
    private readonly INavigationService _navigation;
    public AdminViewModel(INavigationService navigation, Admin admin)
    {
        _navigation = navigation;
    }
}