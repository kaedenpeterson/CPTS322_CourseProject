using System.ComponentModel;
using Avalonia.Controls;

namespace ClassScheduler.CoreUI;

/// <summary>
/// Interface for <see cref="NavigationService"/>.
/// Provides the properties and methods for changing the <see cref="MainView"/>
/// and <see cref="CurrView"/> of the application.
/// </summary>
public interface INavigationService
{
    UserControl MainView { get; }
    UserControl CurrView { get; }
    event PropertyChangedEventHandler PropertyChanged;
    void SwitchTo<T>(object? parameter = null) where T : UserControl;
}