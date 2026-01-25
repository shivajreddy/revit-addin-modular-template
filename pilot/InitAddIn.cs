using Autodesk.Revit.UI;
using Pilot.core.Commands;

namespace Pilot;

/// <summary>
/// Initializes and configures the add-in's ribbon interface in Revit.
/// Creates custom tabs, panels, and buttons that appear in the Revit ribbon.
/// </summary>
/// <remarks>
/// This class is the central location for all UI setup. To add new buttons:
/// <list type="number">
///   <item>Create a new command class in pilot.core/Commands implementing IExternalCommand</item>
///   <item>Add a new PushButtonData in <see cref="InitializeAddInUI"/> referencing that command</item>
///   <item>Optionally use <see cref="Pilot.ui.Revit.RevitPushButton"/> for buttons with icons</item>
/// </list>
/// </remarks>
public class InitAddIn
{
    /// <summary>
    /// Sets up the complete ribbon UI for this add-in.
    /// Called once during Revit startup from <see cref="Main.OnStartup"/>.
    /// </summary>
    /// <param name="app">The Revit UI application instance for creating ribbon elements.</param>
    public static void InitializeAddInUI(UIControlledApplication app)
    {
        // Create a custom Ribbon Tab for this add-in
        const string tabName = "RevitAddInModularTemplate-V1.0.0";
        app.CreateRibbonTab(tabName);

        // Create a panel within the tab to group related commands
        var panel = app.CreateRibbonPanel(tabName, "Commands");

        // Create the Hello World button
        // Note: Assembly path comes from pilot.core since that's where commands are defined
        var helloButtonData = new PushButtonData(
            "HelloWorldButton",                              // Internal unique name
            "Hello\nWorld",                                  // Display text (use \n for line break)
            Pilot.core.CoreAssembly.GetCoreAssemblyLocation(), // Path to assembly containing the command
            HelloWorld.GetPath()                             // Full class name of the command
        )
        {
            ToolTip = "Shows a Hello World message"
        };

        panel.AddItem(helloButtonData);
    }
}