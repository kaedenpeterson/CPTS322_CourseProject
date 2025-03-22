/*
Description: ViewModel for the admin view (logged in) in ClassScheduler.
Author: Kaeden Peterson 11858249
Date: 3-15-25
*/

using ClassScheduler.Models;

namespace ClassScheduler.ViewModels;

public partial class AdminViewModel : ViewModelBase
{
    private readonly ViewManager _navigation;
    public AdminViewModel(Admin admin, ViewManager navigation)
    {
        _navigation = navigation;
    }
}