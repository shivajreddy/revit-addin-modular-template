using Autodesk.Revit.UI;

namespace RevitAddInModularTemplate.UI.Revit;

/// <summary>
/// Stores metadata about a ribbon button for theme-aware image updates.
/// </summary>
public class ButtonMetadata
{
    /// <summary>
    /// The ribbon item (PushButton, SplitButton, etc.)
    /// </summary>
    public RibbonItem Item { get; set; }

    /// <summary>
    /// Base name of the icon (e.g., "hello" for "hello-16-dark.tiff")
    /// </summary>
    public string IconBaseName { get; set; }

    public ButtonMetadata(RibbonItem item, string iconBaseName)
    {
        Item = item;
        IconBaseName = iconBaseName;
    }
}
