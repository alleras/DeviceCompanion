using System.Collections.ObjectModel;

namespace DeviceCompanionAvalonia.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<DeviceCardViewModel> DeviceViews { get; } = [];

    public MainWindowViewModel()
    {
        DeviceViews.Add(new DeviceCardViewModel());
        DeviceViews.Add(new DeviceCardViewModel());
        DeviceViews.Add(new DeviceCardViewModel());
    }
}