using ClassScheduler.CoreUI;
using ClassScheduler.Models;

namespace ClassScheduler.ViewModels;

/// <summary>
/// ViewModel for the admin homepage. Shown as a sub view (CurrView) in the window.
/// </summary>
public partial class AdminViewModel : ViewModelBase
{
    private readonly INavigationService _navigation;
    public AdminViewModel(INavigationService navigation, Admin admin)
    {
        _navigation = navigation;
    }
}