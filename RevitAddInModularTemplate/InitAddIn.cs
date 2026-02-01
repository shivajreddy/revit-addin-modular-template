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
    public static RibbonPanel ribbonPanel;
    //public static PushButtonData helloButtonData;

    /// <summary>
    /// Sets up the ribbon UI. Called once from <see cref="Main.OnStartup"/>.
    /// </summary>
    public static void InitializeAddInUI(UIControlledApplication revitApplication)
    {
        // TODO: create a common list, this list holds all the buttons that are going to be added
        // for the addin.
        // maybe this is a static stateless list, so that commands can add themself into this list
        // and init class goes through the list to create the buttons

        // Create custom ribbon tab
        const string tabName = "RevitAddInModularTemplate-V1.0.0";
        revitApplication.CreateRibbonTab(tabName);

        // Create panel for commands
        ribbonPanel = revitApplication.CreateRibbonPanel(tabName, "Commands");

        // Add Hello World button
        var helloButtonData = new PushButtonData(
            "HelloWorldButton",
            "Hello\nWorld",
            RevitAddInModularTemplate.Core.CoreAssembly.GetCoreAssemblyLocation(),
            HelloWorld.GetPath()
        )
        {
            // 16x16 96dpi
            // 32x32 192dpi
            //Image = RevitAddInModularTemplate.Res.ResourceImage.GetImage("Cube-Red-16-Dark.tiff"),
            //LargeImage = RevitAddInModularTemplate.Res.ResourceImage.GetImage("Cube-Red-16-Dark.tiff"),
            Image = RevitAddInModularTemplate.Res.ResourceImage.GetImage("hello-16-dark.tiff"),
            LargeImage = RevitAddInModularTemplate.Res.ResourceImage.GetImage("hello-32-dark.tiff"),
            ToolTipImage = RevitAddInModularTemplate.Res.ResourceImage.GetImage("hello-32-dark.tiff"),
            ToolTip = "Shows a Hello World message"
        };
        ribbonPanel.AddItem(helloButtonData);
    }

    public void handleThemeChange()
    {
    }
}
