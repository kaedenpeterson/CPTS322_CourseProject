using System.Collections.Generic;
using ClassScheduler.CoreUI;
using ClassScheduler.Models;

namespace ClassScheduler.ViewModels;

public partial class CartViewModel : ViewModelBase
{
    private readonly INavigationService _navigation;
    private readonly Student _student;
    public List<Course> Courses { get; }

    public CartViewModel(INavigationService navigation, Student student)
    { 
        _navigation = navigation;
        _student = student;
        Courses = _student.Cart;
    }
}