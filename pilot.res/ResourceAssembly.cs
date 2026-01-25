using System.Reflection;

namespace Pilot.res;

/// <summary>
/// Resource assembly helper methods
/// </summary>
public class ResourceAssembly
{
    /// <summary>
    /// Gets the current resource assembly
    /// </summary>
    /// <returns></returns>
    public static Assembly GetAssembly()
    {
        return  Assembly.GetExecutingAssembly();
    }

    /// <summary>
    /// Gets the current assembly working namespace
    /// </summary>
    /// <returns></returns>
    public static string GetNamespace()
    {
        return typeof(ResourceAssembly).Namespace + ".";
    }
}
