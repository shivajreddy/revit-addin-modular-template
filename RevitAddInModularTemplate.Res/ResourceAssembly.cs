using System.Reflection;

namespace RevitAddInModularTemplate.Res;

// =============================================================================
// PROJECT: RevitAddInModularTemplate.Res
// =============================================================================
// Embedded resources for the add-in (icons, images, static assets).
//
// To add icons:
//   1. Create folder: Images/Icons/
//   2. Add .ico files 16x16[96dpi] and 32x32[192dpi]
//   helpful video: https://youtu.be/q__JuQDTH_k
//   3. Set Build Action to "Embedded Resource"
//   4. Reference by filename in RevitPushButtonDataModel
//
// Resource path: {Namespace}.{FolderPath}.{FileName}
// Example: RevitAddInModularTemplate.Res.Images.Icons.hello32.ico
// =============================================================================

/// <summary>
/// Provides access to the resource assembly for loading embedded files.
/// </summary>
public class ResourceAssembly
{
    /// <summary>Gets the Res assembly instance.</summary>
    public static Assembly GetAssembly()
    {
        return Assembly.GetExecutingAssembly();
    }

    /// <summary>Gets the namespace prefix for resource paths (with trailing dot).</summary>
    public static string GetNamespace()
    {
        return typeof(ResourceAssembly).Namespace + ".";
    }
}
