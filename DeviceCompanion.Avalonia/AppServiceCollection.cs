using DeviceCompanion.Avalonia.Services;
using DeviceCompanion.Avalonia.ViewModels;
using DeviceCompanion.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DeviceCompanion.Avalonia;

public static class AppServiceCollection
{
    public static void AddAppServices(this IServiceCollection collection)
    {
        collection.AddSingleton<IModulesService, ModulesService>();
        collection.AddSingleton<IPollingManager, PollingManager>();
        
        collection.AddTransient<MainWindowViewModel>();
    }
}