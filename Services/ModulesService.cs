using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using DeviceCompanionAvalonia.Interfaces;
using DeviceCompanionAvalonia.Interfaces.Services;

namespace DeviceCompanionAvalonia.Services
{
    public class ModulesService : IModulesService
    {
        private static ISensorModule LoadDLL(FileInfo file)
        {
            Assembly assembly = Assembly.LoadFile(file.FullName);
            
            var typeName = Path.GetFileNameWithoutExtension(file.Name);
            Type? type = assembly.GetType(typeName + ".Loader") ?? 
                         throw new Exception("Type not found");
            
            object? instance = Activator.CreateInstance(type) ??
                               throw new Exception("Failed to create instance");
            
            MethodInfo? method = type.GetMethod("GetModule") ??
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
            return new List<ISensorModule>(
                [
                ]
            );
        }
    }
}
