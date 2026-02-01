using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using RevitAddInModularTemplate.Core.Commands;
using System.Diagnostics;

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
    private static RibbonPanel ribbonPanel;
    private static RibbonImageThemeSelector ribbonImageThemeSelector;

    /// <summary>
    /// Called when Revit starts. Initializes ribbon UI (tabs, panels, buttons).
    /// </summary>
    public Result OnStartup(UIControlledApplication revitApplication)
    {
        //InitAddIn.InitializeAddInUI(revitApplication);
        // TODO: handle theme change logic
        //revitApplication.ThemeChanged += OnThemeChange;
        ribbonImageThemeSelector = new RibbonImageThemeSelector(revitApplication);
        ribbonPanel = revitApplication.CreateRibbonPanel("example");

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
        var x = ribbonPanel.GetItems();
        //ribbonImageThemeSelector.ribbonItems.Add(ribbonPanel.GetItems());
        ribbonImageThemeSelector.AddRibbonItemms(ribbonPanel.GetItems());

        return Result.Succeeded;
    }



    /// <summary>
    /// Called when Revit shuts down. Use for cleanup and resource disposal.
    /// </summary>
    public Result OnShutdown(UIControlledApplication application)
    {
        //ribbonPanel.Remove();
        ribbonImageThemeSelector.Dispose();
        return Result.Succeeded;
    }
}

public class RibbonImageThemeSelector :IDisposable
{
    private readonly UIControlledApplication revitApplication;
    public List<RibbonItem> ribbonItems = new List<RibbonItem>();
    public RibbonImageThemeSelector(UIControlledApplication application)
    {
        this.revitApplication = application;
        this.revitApplication.ThemeChanged += OnThemeChange;
    }
    internal void AddRibbonItemms(IList<RibbonItem> ribbonItems)
    {
        foreach(var item in ribbonItems)
        {
            AddRibbonItem(item);
        }
        //throw new NotImplementedException();
    }
    public void AddRibbonItem(RibbonItem item)
    {
        ribbonItems.Add(item);
    }
    public void OnThemeChange(object sender, ThemeChangedEventArgs args)
    {
        Debug.WriteLine("THEME CHANGED, toggle buttons");
        UpdateImages();
    }
    public void UpdateImages()
    {
        foreach (var ribbonItem  in ribbonItems)
        {
            Debug.WriteLine("Going to change image for:", ribbonItem.Name);
        //InitAddIn.helloButtonData.Image = RevitAddInModularTemplate.Res.ResourceImage.GetImage("hello-16-light.tiff");
        //InitAddIn.helloButtonData.LargeImage = RevitAddInModularTemplate.Res.ResourceImage.GetImage("hello-32-light.tiff");
        }
    }

    public void Dispose()
    {
        //this.revitApplication.
        throw new NotImplementedException();
    }

}
