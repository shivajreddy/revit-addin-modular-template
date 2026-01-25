using Autodesk.Revit.UI;

namespace Pilot;

// =============================================================================
// PROJECT: pilot
// =============================================================================
// The main entry point project for the Revit add-in. This project is responsible
// for bootstrapping the add-in when Revit starts and setting up the ribbon UI.
//
// Dependencies:
//   - pilot.core: Contains commands and business logic
//   - pilot.ui:   Contains UI helper classes for ribbon creation
//   - pilot.res:  Contains embedded resources (icons, images)
//
// The .addin manifest file references this assembly and the Main class below.
// =============================================================================

/// <summary>
/// The main entry point for the Revit add-in.
/// Implements <see cref="IExternalApplication"/> to hook into Revit's startup/shutdown lifecycle.
/// </summary>
/// <remarks>
/// This class is referenced in the .addin manifest file (Pilot.addin) as the entry point.
/// When Revit loads, it calls <see cref="OnStartup"/> to initialize the add-in,
/// and <see cref="OnShutdown"/> when Revit closes to perform cleanup.
/// </remarks>
public class Main : IExternalApplication
{
    /// <summary>
    /// Called when Revit starts up and loads this add-in.
    /// Initializes the ribbon UI including tabs, panels, and buttons.
    /// </summary>
    /// <param name="currentRevitApplication">
    /// The Revit application instance used to create UI elements.
    /// </param>
    /// <returns>
    /// <see cref="Result.Succeeded"/> if initialization was successful,
    /// <see cref="Result.Failed"/> otherwise.
    /// </returns>
    public Result OnStartup(UIControlledApplication currentRevitApplication)
    {
        InitAddIn.InitializeAddInUI(currentRevitApplication);
        return Result.Succeeded;
    }

    /// <summary>
    /// Called when Revit shuts down.
    /// Use this method to release resources, save settings, or perform cleanup.
    /// </summary>
    /// <param name="application">The Revit application instance.</param>
    /// <returns><see cref="Result.Succeeded"/> after cleanup completes.</returns>
    public Result OnShutdown(UIControlledApplication application)
    {
        return Result.Succeeded;
    }
}
