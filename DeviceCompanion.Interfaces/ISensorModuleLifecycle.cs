using System;
using System.Threading.Tasks;
using DeviceCompanion.Interfaces.Models;

namespace DeviceCompanionAvalonia.Interfaces
{
    public interface ISensorModuleLifecycle
    {
        public Task StartPolling(Action<SensorData> onDataReceived);
        public void StopPolling(); // Stop the polling process
    }
}
