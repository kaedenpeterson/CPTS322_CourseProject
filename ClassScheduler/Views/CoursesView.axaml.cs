/*
Description: UI for admin view (logged in) in ClassScheduler
Author: Bitna White 11812714
Date: 3-15-25
*/

using Avalonia.Controls;
using ClassScheduler.ViewModels;

namespace ClassScheduler.Views;

public partial class CoursesView : UserControl
{
    public CoursesView(ViewManager navigation)
    {
        InitializeComponent();
        DataContext = new CoursesViewModel(navigation);
    }
}