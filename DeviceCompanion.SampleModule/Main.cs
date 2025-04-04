using System;
using System.Threading.Tasks;
using DeviceCompanion.Interfaces;
using DeviceCompanion.Interfaces.Models;
using DeviceCompanionAvalonia.Interfaces;

namespace DeviceCompanion.SampleModule
{
    public class SampleModule2 : ISensorModule, ISensorModuleLifecycle
    {
        public Guid Id { get; set; }
        public string Name => "Sample Module 2";
        public string Description => "Sample Module 2 - Description";
        public string Version => "1.0.0";
        public SensorState State {
            get => _state;
            private set
            {
                var oldState = _state;
                _state = value;
                ModuleStateChanged?.Invoke(this, new ModuleStateChangedEventArgs(oldState, value));
            }
        }
        private SensorState _state;

        public event EventHandler<ModuleStateChangedEventArgs>? ModuleStateChanged;

        private Action<SensorData>? _onDataReceived;
        private readonly int _pollInterval = 1000;

        public async Task StartPolling(Action<SensorData> onDataReceived)
        {
            if (State.IsPolling)
            {
                throw new Exception("Cannot start polling. Already polling");
            }

            State = State.CopyWith(isConnecting: true);
            await Task.Delay(3000);

            _onDataReceived = onDataReceived;

            State = State.CopyWith(isConnected: true, isPolling: true, isConnecting: false);
            _ = Poll();
        }

        public void StopPolling()
        {
            State = State.CopyWith(isPolling: false);
        }

        private async Task Poll()
        {
            if (_onDataReceived == null)
            {
                throw new InvalidOperationException("_onDataReceived was not defined. Cannot poll");
            }

            while (State.IsPolling)
            {
                _onDataReceived(new SensorData
                {
                    Timestamp = DateTime.Now,
                    Value = Random.Shared.Next()
                });

                await Task.Delay(_pollInterval); // Every second. Should be configurable
            }
        }
    }
}
