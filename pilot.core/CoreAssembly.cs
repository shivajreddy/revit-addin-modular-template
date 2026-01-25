using System.Reflection;

namespace Pilot.core;

/// <summary>
/// The core assembly
/// </summary>
public class CoreAssembly
{

    public static string GetCoreAssemblyLocation()
    {
        return Assembly.GetExecutingAssembly().Location;
    }

}
