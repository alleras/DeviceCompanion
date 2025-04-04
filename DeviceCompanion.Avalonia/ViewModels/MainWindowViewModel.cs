using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DeviceCompanion.Avalonia.Services;
using DeviceCompanion.Interfaces.Services;

namespace DeviceCompanion.Avalonia.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<DeviceCardViewModel> DeviceViews { get; } = [];

    public MainWindowViewModel(IPollingManager pollingManager)
    {
        foreach (var module in pollingManager.ModuleInstances)
        {
            DeviceViews.Add(new DeviceCardViewModel(module));
        }
        
        pollingManager.StartAllPolling();
    }
}