using Autodesk.Revit.UI;

namespace RevitAddInModularTemplate;

// =============================================================================
// PROJECT: RevitAddInModularTemplate (Main Entry Point)
// =============================================================================
// Bootstraps the add-in when Revit starts and sets up the ribbon UI.
//
// Dependencies:
//   - RevitAddInModularTemplate.Core: Commands and business logic
//   - RevitAddInModularTemplate.UI:   Ribbon UI helper classes
//   - RevitAddInModularTemplate.Res:  Embedded resources (icons, images)
// =============================================================================

/// <summary>
/// Main entry point for the Revit add-in, implementing <see cref="IExternalApplication"/>.
/// Referenced in the .addin manifest file as the startup class.
/// </summary>
public class Main : IExternalApplication
{
    /// <summary>
    /// Called when Revit starts. Initializes ribbon UI (tabs, panels, buttons).
    /// </summary>
    public Result OnStartup(UIControlledApplication application)
    {
        InitAddIn.InitializeAddInUI(application);
        return Result.Succeeded;
    }

    /// <summary>
    /// Called when Revit shuts down. Use for cleanup and resource disposal.
    /// </summary>
    public Result OnShutdown(UIControlledApplication application)
    {
        return Result.Succeeded;
    }
}
