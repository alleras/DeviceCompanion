using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeviceCompanionAvalonia.Interfaces.Services
{
    public interface IModulesService
    {
        public Task<IEnumerable<ISensorModule>> GetInstalledModules();
    }
}
