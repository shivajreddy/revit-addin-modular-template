using Autodesk.Revit.UI;

namespace RevitAddInModularTemplate.UI.Revit;

// =============================================================================
// PROJECT: RevitAddInModularTemplate.UI
// =============================================================================
// UI helper classes for creating Revit ribbon elements.
//
// Dependencies:
//   - RevitAddInModularTemplate.Core: Assembly path for command registration
//   - RevitAddInModularTemplate.Res:  Embedded icon images
// =============================================================================

/// <summary>
/// Data model for creating a Revit push button with <see cref="RevitPushButton.Create"/>.
/// </summary>
public class RevitPushButtonDataModel
{
    /// <summary>Button text. Use \n for multi-line.</summary>
    public string Label { get; set; }

    /// <summary>Target ribbon panel.</summary>
    public RibbonPanel Panel { get; set; }

    /// <summary>Full command class name (use Command.GetPath()).</summary>
    public string CommandNamespacePath { get; set; }

    /// <summary>Short tooltip text.</summary>
    public string Tooltip { get; set; }

    /// <summary>Extended tooltip description.</summary>
    public string LongDescription { get; set; }

    /// <summary>Tooltip image filename (embedded resource).</summary>
    public string TooltipImageName { get; set; }

    /// <summary>Small icon filename (16x16) in Res/Images/Icons/.</summary>
    public string IconImageName { get; set; }

    /// <summary>Large icon filename (32x32) in Res/Images/Icons/.</summary>
    public string IconLargeImageName { get; set; }

    public RevitPushButtonDataModel() { }
}
