using System.Reflection;

namespace Pilot.core;

// =============================================================================
// PROJECT: pilot.core
// =============================================================================
// Contains the core business logic and command implementations for the add-in.
// All IExternalCommand implementations (Revit commands) should be placed here.
//
// Namespaces:
//   - Pilot.core:          Assembly utilities and shared core functionality
//   - Pilot.core.Commands: All Revit command implementations (IExternalCommand)
//
// This assembly is referenced by pilot.ui for command registration and by
// the pilot project for wiring up buttons to commands.
// =============================================================================

/// <summary>
/// Provides utilities for accessing the core assembly's metadata and location.
/// Used primarily for registering commands with the Revit ribbon.
/// </summary>
/// <remarks>
/// When creating a <see cref="Autodesk.Revit.UI.PushButtonData"/>, Revit requires
/// the full path to the assembly containing the command. Since commands live in
/// pilot.core, this class provides that path via <see cref="GetCoreAssemblyLocation"/>.
/// </remarks>
public class CoreAssembly
{
    /// <summary>
    /// Gets the file path of the pilot.core.dll assembly.
    /// </summary>
    /// <returns>
    /// The full file system path to the executing assembly (pilot.core.dll).
    /// Used when registering commands with Revit's PushButtonData.
    /// </returns>
    /// <example>
    /// <code>
    /// var buttonData = new PushButtonData(
    ///     "MyButton",
    ///     "Click Me",
    ///     CoreAssembly.GetCoreAssemblyLocation(),  // Path to pilot.core.dll
    ///     "Pilot.core.Commands.MyCommand"          // Command class name
    /// );
    /// </code>
    /// </example>
    public static string GetCoreAssemblyLocation()
    {
        return Assembly.GetExecutingAssembly().Location;
    }
}
