using DeviceCompanion.Interfaces;

namespace DeviceCompanion.SampleModule;

public class Loader
{
    public ISensorModule GetModule()
    {
        return new SampleModule2();
    }
}