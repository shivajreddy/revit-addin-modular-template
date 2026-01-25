using Autodesk.Revit.UI;

namespace Pilot.ui.Revit;

// =============================================================================
// PROJECT: pilot.ui
// =============================================================================
// Contains UI helper classes for creating Revit ribbon elements.
// Provides factory methods and data models to simplify button creation.
//
// Namespaces:
//   - Pilot.ui.Revit: Helper classes for Revit ribbon UI (buttons, panels)
//
// Dependencies:
//   - pilot.core: For assembly path when registering commands
//   - pilot.res:  For loading embedded icon images
// =============================================================================

/// <summary>
/// Data model containing all properties needed to create a Revit push button.
/// Used with <see cref="RevitPushButton.Create"/> to generate buttons with icons.
/// </summary>
/// <remarks>
/// This model encapsulates all button configuration in one place, making it
/// easier to create consistent buttons throughout the add-in.
/// </remarks>
/// <example>
/// <code>
/// var buttonData = new RevitPushButtonDataModel
/// {
///     Label = "My Command",
///     Panel = myPanel,
///     CommandNamespacePath = MyCommand.GetPath(),
///     Tooltip = "Executes my command",
///     IconImageName = "MyIcon16.ico",
///     IconLargeImageName = "MyIcon32.ico"
/// };
/// RevitPushButton.Create(buttonData);
/// </code>
/// </example>
public class RevitPushButtonDataModel
{
    /// <summary>
    /// The text displayed on the button. Use \n for multi-line labels.
    /// </summary>
    public string Label { get; set; }

    /// <summary>
    /// The ribbon panel where this button will be added.
    /// </summary>
    public RibbonPanel Panel { get; set; }

    /// <summary>
    /// The fully qualified class name of the command (e.g., "Pilot.core.Commands.HelloWorld").
    /// Use the command's GetPath() method to get this value.
    /// </summary>
    public string CommandNamespacePath { get; set; }

    /// <summary>
    /// Short tooltip text shown when hovering over the button.
    /// </summary>
    public string Tooltip { get; set; }

    /// <summary>
    /// Extended description shown in the expanded tooltip.
    /// </summary>
    public string LongDescription { get; set; }

    /// <summary>
    /// Filename of the tooltip image (embedded resource in pilot.res).
    /// </summary>
    public string TooltipImageName { get; set; }

    /// <summary>
    /// Filename of the small icon (16x16 pixels) for compact button display.
    /// Must be an embedded resource in pilot.res/Images/Icons/.
    /// </summary>
    public string IconImageName { get; set; }

    /// <summary>
    /// Filename of the large icon (32x32 pixels) for standard button display.
    /// Must be an embedded resource in pilot.res/Images/Icons/.
    /// </summary>
    public string IconLargeImageName { get; set; }

    /// <summary>
    /// Creates a new empty button data model.
    /// </summary>
    public RevitPushButtonDataModel() { }
}
