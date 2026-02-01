using Autodesk.Revit.UI;

namespace RevitAddInModularTemplate.UI.Revit;

/// <summary>
/// Factory for creating Revit ribbon push buttons with icons.
/// </summary>
/// <remarks>
/// Use for buttons with custom icons. Icons must be embedded resources
/// in RevitAddInModularTemplate.Res/Images/Icons/.
/// </remarks>
public class RevitPushButton
{
    /// <summary>
    /// Creates a push button with full icon support and adds it to the panel.
    /// </summary>
    public static PushButton Create(RevitPushButtonDataModel data)
    {
        var btnDataName = Guid.NewGuid().ToString();

        var btnData = new PushButtonData(
            btnDataName,
            data.Label,
            RevitAddInModularTemplate.Core.CoreAssembly.GetCoreAssemblyLocation(),
            data.CommandNamespacePath)
        {
            Image = RevitAddInModularTemplate.Res.ResourceImage.GetImage(data.IconImageName),
            LargeImage = RevitAddInModularTemplate.Res.ResourceImage.GetImage(data.IconLargeImageName),
            ToolTipImage = RevitAddInModularTemplate.Res.ResourceImage.GetImage(data.IconLargeImageName),
            ToolTip = data.Tooltip,
            LongDescription = data.LongDescription,
        };

        return data.Panel.AddItem(btnData) as PushButton;
    }
}
