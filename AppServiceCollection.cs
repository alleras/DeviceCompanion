using DeviceCompanionAvalonia.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace DeviceCompanionAvalonia;

public static class AppServiceCollection
{
    public static void AddAppServices(this IServiceCollection collection)
    {
        collection.AddTransient<MainWindowViewModel>();
    }
}