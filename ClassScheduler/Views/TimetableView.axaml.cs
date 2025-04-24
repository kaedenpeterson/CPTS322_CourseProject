using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.VisualTree;
using ClassScheduler.CoreUI;
using ClassScheduler.Models;
using ClassScheduler.ViewModels;
using System;
namespace ClassScheduler.Views;

public partial class TimetableView : UserControl
{
    private readonly TimetableViewModel _viewModel;

    public TimetableView(INavigationService navigation, Student student)
    {
        InitializeComponent();

        _viewModel = new TimetableViewModel(navigation, student);
        DataContext = _viewModel;

        BuildTimetable();
    }

    private void BuildTimetable()
    {
        var grid = this.FindControl<Grid>("TimeTableGrid");
        grid.Children.Clear();
        grid.RowDefinitions.Clear();
        grid.ColumnDefinitions.Clear();

        for (int col = 0; col <= 5; col++)
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));
        }

        TimeSpan start = new TimeSpan(7, 0, 0);
        TimeSpan end = new TimeSpan(20, 0, 0);

        int totalHours = (int)(end - start).TotalHours;

        for (int i = 0; i <= totalHours; i++)
        {
            grid.RowDefinitions.Add(new RowDefinition(new GridLength(60)));

            var time = start.Add(TimeSpan.FromHours(i));
            var timeLabel = new TextBlock
            {
                Text = time.ToString(@"hh\:mm"),
                FontSize = 12,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 6, 0)
            };

            Grid.SetRow(timeLabel, i);
            Grid.SetColumn(timeLabel, 0);
            grid.Children.Add(timeLabel);
        }

        foreach (var course in _viewModel.EnrolledCourses)
        {
            foreach (var day in course.Schedule.Days)
            {
                int col = day switch
                {
                    DayOfWeek.Monday => 1,
                    DayOfWeek.Tuesday => 2,
                    DayOfWeek.Wednesday => 3,
                    DayOfWeek.Thursday => 4,
                    DayOfWeek.Friday => 5,
                    _ => -1
                };

                if (col == -1) continue;

                var startTime = course.Schedule.StartTime;
                var endTime = course.Schedule.EndTime;

                int startRow = Math.Max(0, (int)(startTime - start).TotalHours);
                int span = Math.Max(1, (int)(endTime - startTime).TotalHours);

                var block = new Border
                {
                    Background = Brushes.SteelBlue,
                    CornerRadius = new CornerRadius(8),
                    Margin = new Thickness(2),
                    Padding = new Thickness(4),
                    Child = new TextBlock
                    {
                        Text = $"{course.Code}\n{course.Name}\n{course.Schedule.StartTime:hh\\:mm} - {course.Schedule.EndTime:hh\\:mm}",
                        FontSize = 11,
                        TextAlignment = TextAlignment.Center,
                        VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center
                    }
                };

                Grid.SetColumn(block, col);
                Grid.SetRow(block, startRow);
                Grid.SetRowSpan(block, span);
                grid.Children.Add(block);
            }
        }
    }
}
