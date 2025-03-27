using System;
using Avalonia.Controls;

namespace ClassScheduler.CoreUI;

/// <summary>
/// Code-behind for PopupWindow.axaml.
/// Defines a template that is used for creating popup windows.
/// </summary>
public partial class PopupWindow : Window
{
    public string Message { get; set; }
    
    public PopupWindow(string title, string message, string primaryButtonText, Action? primaryAction = null,
                        string? secondaryButtonText = null, Action? secondaryAction = null)
    {
        InitializeComponent();
        
        Title = title;
        Message = message;
        
        PrimaryButton.Content = primaryButtonText;
        PrimaryButton.Click += (_, _) =>
        {
            primaryAction?.Invoke();
            Close();
        };

        if (!string.IsNullOrEmpty(secondaryButtonText))
        {
            SecondaryButton.Content = secondaryButtonText;
            SecondaryButton.Click += (_, _) =>
            {
                secondaryAction?.Invoke();
                Close();
            };
        }
        else
        {
            SecondaryButton.IsVisible = false;
        }
        
        DataContext = this;
    }
}