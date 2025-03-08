using System;
using DeviceCompanionAvalonia.Models;

namespace DeviceCompanionAvalonia.Interfaces
{
    public class ModuleStateChangedEventArgs(SensorState previousState, SensorState currentState) : EventArgs
    {
        public SensorState PreviousState { get; } = previousState;
        public SensorState CurrentState { get; } = currentState;
    }
    public interface ISensorModule
    {
        public Guid Id { get; set; }
        public string Name { get; } 
        public string Description { get; }
        public string Version { get; }
        public SensorState State { get; }
        //public ViewModelBase GetConfigurationUI();
        public event EventHandler<ModuleStateChangedEventArgs>? ModuleStateChanged;
    }
}
