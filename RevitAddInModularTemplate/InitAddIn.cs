using Autodesk.Revit.UI;
using RevitAddInModularTemplate.Core.Commands;

namespace RevitAddInModularTemplate;

/// <summary>
/// Initializes the add-in's ribbon interface (tabs, panels, buttons).
/// </summary>
/// <remarks>
/// To add new buttons:
/// 1. Create a command in RevitAddInModularTemplate.Core/Commands implementing IExternalCommand
/// 2. Add PushButtonData here referencing that command
/// 3. Use <see cref="UI.Revit.RevitPushButton"/> for buttons with icons
/// </remarks>
public class InitAddIn
{
    /// <summary>
    /// Sets up the ribbon UI. Called once from <see cref="Main.OnStartup"/>.
    /// </summary>
    public static void InitializeAddInUI(UIControlledApplication app)
    {
        // Create custom ribbon tab
        const string tabName = "RevitAddInModularTemplate-V1.0.0";
        app.CreateRibbonTab(tabName);

        // Create panel for commands
        var panel = app.CreateRibbonPanel(tabName, "Commands");

        // Add Hello World button
        var helloButtonData = new PushButtonData(
            "HelloWorldButton",
            "Hello\nWorld",
            RevitAddInModularTemplate.Core.CoreAssembly.GetCoreAssemblyLocation(),
            HelloWorld.GetPath()
        )
        {
            ToolTip = "Shows a Hello World message"
        };

        panel.AddItem(helloButtonData);
    }
}
