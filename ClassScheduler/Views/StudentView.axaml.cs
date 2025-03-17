/*
Description: Code-behind for the student view (logged in) in ClassScheduler.
Author: Kaeden Peterson 11858249
Date: 3-15-25
*/

using Avalonia.Interactivity;
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

    public void ClickHandler(object sender, RoutedEventArgs args)
    {
        instructor.Text = "Instructor: Andy O'Fallon"; //these hard coded strings are just a proof of concept
        time.Text = "Time: 3 am";                      //perhaps in the future we can have them be variables to the class
        credits.Text = "Credits: 4";
        prereqs.Text = "Prerequisites: none";

    }
}