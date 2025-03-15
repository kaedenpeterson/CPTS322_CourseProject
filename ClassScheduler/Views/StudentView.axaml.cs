/*
Description: Code-behind for the student view (logged in) in ClassScheduler.
Author: Kaeden Peterson 11858249
Date: 3-15-25
*/

using Avalonia.Controls;
using ClassScheduler.Models;
using ClassScheduler.ViewModels;

namespace ClassScheduler.Views;

public partial class StudentView : Window
{
    public StudentView(Student student)
    {
        InitializeComponent();
        DataContext = new StudentViewModel(student);
    }
}