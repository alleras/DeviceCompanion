namespace DeviceCompanion.Interfaces.Services
{
    public interface IPollingManager
    {
        public IReadOnlyCollection<ISensorModule> ModuleInstances { get; }

        public Task Initialize();
        public void StartAllPolling();
        public void StopPolling(ISensorModule module);
        public void StartPolling(ISensorModule module);
    }
}
