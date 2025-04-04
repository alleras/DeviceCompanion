namespace DeviceCompanion.Interfaces.Services
{
    public interface IModulesService
    {
        public Task<IEnumerable<ISensorModule>> GetInstalledModules();
    }
}
