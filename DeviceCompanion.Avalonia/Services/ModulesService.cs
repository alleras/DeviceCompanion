using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DeviceCompanion.Interfaces;
using DeviceCompanion.Interfaces.Services;

namespace DeviceCompanion.Avalonia.Services
{
    public class ModulesService : IModulesService
    {
        private static ISensorModule LoadDll(FileInfo file)
        {
            var assembly = Assembly.LoadFile(file.FullName);
            
            var typeName = Path.GetFileNameWithoutExtension(file.Name);
            var type = assembly.GetType(typeName + ".Loader") ?? 
                         throw new Exception("Type not found");
            
            var instance = Activator.CreateInstance(type) ??
                               throw new Exception("Failed to create instance");
            
            var method = type.GetMethod("GetModule") ??
                                 throw new Exception("Method not found");
            
            var module = method.Invoke(instance, null) ?? 
                                   throw new Exception("GetModule returned null");

            if (module is not ISensorModule sensorModule)
            {
                throw new Exception($"Failed to create sensor module");
            }

            return sensorModule;
        }
        
        public async Task<IEnumerable<ISensorModule>> GetInstalledModules()
        {
            var dirInfo = new DirectoryInfo(Directory.GetCurrentDirectory() + "\\modules");

            var files = dirInfo.EnumerateFiles();

            return files.Select(LoadDll).ToList();
        }
    }
}
