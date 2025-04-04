using CommunityToolkit.Mvvm.ComponentModel;
using DeviceCompanion.Interfaces;

namespace DeviceCompanion.Avalonia.ViewModels;

public enum DeviceStatusEnum
{
    Unknown,
    Connecting,
    Connected,
    Stopped
}
public partial class DeviceCardViewModel : ViewModelBase
{
    [ObservableProperty] private string _deviceName = "Default Name";
    [ObservableProperty] private string _deviceDescription = "Description goes here.";
    [ObservableProperty] private string _actionButtonText = "Start";
    [ObservableProperty] private DeviceStatusEnum _status = DeviceStatusEnum.Unknown;
    [ObservableProperty] private string _statusText = "Not set";
    partial void OnStatusChanged(DeviceStatusEnum value) => StatusText = value.ToString();
    
    private readonly ISensorModule? _sensorModule;
    public DeviceCardViewModel() { }

    public DeviceCardViewModel(ISensorModule module)
    {
        _sensorModule = module;
        _sensorModule.ModuleStateChanged += OnModuleStateChanged;

        _deviceName = _sensorModule.Name;
        _deviceDescription = _sensorModule.Description;
        
        OnModuleStateChanged(null, new ModuleStateChangedEventArgs(_sensorModule.State, _sensorModule.State));
    }
    
    private void OnModuleStateChanged(object? sender, ModuleStateChangedEventArgs e)
    {
        if (sender is null) { }
        var state = e.CurrentState;

        Status = state switch
        {
            { IsConnecting: true, IsConnected: false, IsPolling: false } => DeviceStatusEnum.Connecting,
            { IsConnecting: false, IsConnected: true, IsPolling: true } => DeviceStatusEnum.Connected,
            { IsConnecting: false, IsConnected: false, IsPolling: false } => DeviceStatusEnum.Stopped,
            _ => DeviceStatusEnum.Unknown
        };
    }
}