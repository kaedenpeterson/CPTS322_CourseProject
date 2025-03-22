namespace ClassScheduler.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private ViewManager Navigation { get; }

    public object CurrView => Navigation.CurrView;

    public MainWindowViewModel(ViewManager navigation)
    {
        Navigation = navigation;
        
        Navigation.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(Navigation.CurrView))
            {
                OnPropertyChanged(nameof(CurrView));
            }
        };
    }
}