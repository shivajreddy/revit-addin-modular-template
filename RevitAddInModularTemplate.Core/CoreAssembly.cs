using System.Reflection;

namespace RevitAddInModularTemplate.Core;

// =============================================================================
// PROJECT: RevitAddInModularTemplate.Core
// =============================================================================
// Core business logic and command implementations for the add-in.
// All IExternalCommand implementations should be placed here.
//
// Namespaces:
//   - RevitAddInModularTemplate.Core:          Assembly utilities
//   - RevitAddInModularTemplate.Core.Commands: Revit command implementations
// =============================================================================

/// <summary>
/// Provides the assembly path for command registration with Revit ribbon.
/// </summary>
public class CoreAssembly
{
    /// <summary>
    /// Gets the file path to the Core assembly. Required for PushButtonData registration.
    /// </summary>
    public static string GetCoreAssemblyLocation()
    {
        return Assembly.GetExecutingAssembly().Location;
    }
}
