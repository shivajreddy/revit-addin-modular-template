using System.Reflection;

namespace Pilot.res;

// =============================================================================
// PROJECT: pilot.res
// =============================================================================
// Contains all embedded resources for the add-in, including:
//   - Icons for ribbon buttons (16x16 and 32x32 .ico files)
//   - Images for tooltips
//   - Any other static assets
//
// Namespaces:
//   - Pilot.res: Resource access utilities
//
// To add new icons:
//   1. Create folder structure: Images/Icons/
//   2. Add .ico files to that folder
//   3. Set Build Action to "Embedded Resource" in file properties
//   4. Reference icons by filename in RevitPushButtonDataModel
//
// Resource naming convention:
//   Embedded resources are named: {DefaultNamespace}.{FolderPath}.{FileName}
//   Example: Pilot.res.Images.Icons.hello32.ico
// =============================================================================

/// <summary>
/// Provides access to the resource assembly and its metadata.
/// Used by <see cref="ResourceImage"/> to locate embedded resources.
/// </summary>
public class ResourceAssembly
{
    /// <summary>
    /// Gets the pilot.res assembly instance.
    /// </summary>
    /// <returns>The Assembly object for pilot.res.dll.</returns>
    public static Assembly GetAssembly()
    {
        return Assembly.GetExecutingAssembly();
    }

    /// <summary>
    /// Gets the base namespace prefix for embedded resources.
    /// </summary>
    /// <returns>
    /// The namespace with a trailing dot (e.g., "Pilot.res.").
    /// This is prepended to resource paths when loading embedded files.
    /// </returns>
    public static string GetNamespace()
    {
        return typeof(ResourceAssembly).Namespace + ".";
    }
}
