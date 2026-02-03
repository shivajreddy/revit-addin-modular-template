using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using RevitAddInModularTemplate.Core.Commands;
using RevitAddInModularTemplate.UI.Revit;
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
        ribbonImageThemeSelector = new RibbonImageThemeSelector(revitApplication);

        const string tabName = "RAT-V1.0.0";
        revitApplication.CreateRibbonTab(tabName);
        ribbonPanel = revitApplication.CreateRibbonPanel(tabName, "Commands");

        // Create Hello World button with theme-aware images
        var helloButtonData = new PushButtonData(
            "HelloWorldButton",
            "Hello\nWorld",
            RevitAddInModularTemplate.Core.CoreAssembly.GetCoreAssemblyLocation(),
            HelloWorld.GetPath()
        )
        {
            // Use ThemeManager to get images for current theme
            Image = Res.ThemeManager.GetSmallIcon("hello"),
            LargeImage = Res.ThemeManager.GetLargeIcon("hello"),
            ToolTipImage = Res.ThemeManager.GetLargeIcon("hello"),
            ToolTip = "Shows a Hello World message"
        };
        var helloButton = ribbonPanel.AddItem(helloButtonData) as PushButton;

        // Register button for theme updates
        ribbonImageThemeSelector.RegisterButton(helloButton, "hello");

        return Result.Succeeded;
    }

    /// <summary>
    /// Called when Revit shuts down. Use for cleanup and resource disposal.
    /// </summary>
    public Result OnShutdown(UIControlledApplication application)
    {
        ribbonImageThemeSelector?.Dispose();
        return Result.Succeeded;
    }
}

/// <summary>
/// Manages automatic theme switching for ribbon buttons.
/// Subscribes to Revit's ThemeChanged event and updates button images accordingly.
/// </summary>
public class RibbonImageThemeSelector : IDisposable
{
    private readonly UIControlledApplication revitApplication;
    private readonly List<ButtonMetadata> buttonMetadataList = new List<ButtonMetadata>();

    public RibbonImageThemeSelector(UIControlledApplication application)
    {
        this.revitApplication = application;
        this.revitApplication.ThemeChanged += OnThemeChange;
    }

    /// <summary>
    /// Registers a button for automatic theme updates.
    /// </summary>
    /// <param name="button">The ribbon button to track</param>
    /// <param name="iconBaseName">Base name of the icon (e.g., "hello")</param>
    public void RegisterButton(RibbonItem button, string iconBaseName)
    {
        buttonMetadataList.Add(new ButtonMetadata(button, iconBaseName));
    }

    private void OnThemeChange(object sender, ThemeChangedEventArgs args)
    {
        Debug.WriteLine($"Theme changed to: {args.ThemeChangedType}");
        
        // Update theme manager with new theme
        Res.ThemeManager.UpdateTheme(args);
        
        // Update all button images
        UpdateImages();
    }

    /// <summary>
    /// Updates all registered button images to match the current theme.
    /// </summary>
    private void UpdateImages()
    {
        foreach (var metadata in buttonMetadataList)
        {
            try
            {
                if (metadata.Item is PushButton pushButton)
                {
                    pushButton.Image = Res.ThemeManager.GetSmallIcon(metadata.IconBaseName);
                    pushButton.LargeImage = Res.ThemeManager.GetLargeIcon(metadata.IconBaseName);
                    pushButton.ToolTipImage = Res.ThemeManager.GetLargeIcon(metadata.IconBaseName);
                    
                    Debug.WriteLine($"Updated images for button: {pushButton.Name} (theme: {Res.ThemeManager.CurrentTheme})");
                }
                // Future: Add support for SplitButton, PulldownButton, etc.
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error updating images for {metadata.Item.Name}: {ex.Message}");
            }
        }
    }

    public void Dispose()
    {
        this.revitApplication.ThemeChanged -= OnThemeChange;
        buttonMetadataList.Clear();
    }
}
