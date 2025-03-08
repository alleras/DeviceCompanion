using System;
using System.Collections.Generic;
using DeviceCompanionAvalonia.Interfaces;
using DeviceCompanionAvalonia.Interfaces.Services;
using DeviceCompanionAvalonia.Models;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace DeviceCompanionAvalonia.Services
{
    public class PollingManager : IPollingManager
    {
        private readonly IModulesService _modulesService;
        private readonly List<ISensorModule> _moduleInstances = [];
        private readonly Dictionary<ISensorModule, CancellationTokenSource> _cancellationTokens = [];

        public IReadOnlyCollection<ISensorModule> ModuleInstances => _moduleInstances.AsReadOnly();

        public PollingManager(IModulesService modulesService)
        {
            _modulesService = modulesService;
            _ = Initialize();
        }
        public async Task Initialize()
        {
            _moduleInstances.AddRange(await _modulesService.GetInstalledModules());
            foreach (var module in _moduleInstances)
            {
                module.Id = Guid.NewGuid();
            }
        }

        public void StartAllPolling()
        {
            foreach (var module in _moduleInstances)
            {
                StartPolling(module);
            }
        }

        public void StopPolling(ISensorModule module)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            (module as ISensorModuleLifecycle)!.StopPolling();
        }

        public void StartPolling(ISensorModule module)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            (module as ISensorModuleLifecycle)!.StartPolling(OnDataReceived);
        }
        private void OnDataReceived(SensorData data)
        {
            Debug.WriteLine(data.Value.ToString(CultureInfo.InvariantCulture));
        }
    }
}
