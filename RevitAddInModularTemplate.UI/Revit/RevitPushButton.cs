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
    /// Use CreateWithTheme for automatic theme switching support.
    /// </summary>
    public static PushButton Create(RevitPushButtonDataModel data)
    {
        var btnDataName = Guid.NewGuid().ToString();

        var btnData = new PushButtonData(
            btnDataName,
            data.Label,
            RevitAddInModularTemplate.Core.CoreAssembly.GetCoreAssemblyLocation(),
            data.CommandNamespacePath);

        // Use IconBaseName for theme-aware images if provided
        if (!string.IsNullOrEmpty(data.IconBaseName))
        {
            btnData.Image = RevitAddInModularTemplate.Res.ThemeManager.GetSmallIcon(data.IconBaseName);
            btnData.LargeImage = RevitAddInModularTemplate.Res.ThemeManager.GetLargeIcon(data.IconBaseName);
            btnData.ToolTipImage = RevitAddInModularTemplate.Res.ThemeManager.GetLargeIcon(data.IconBaseName);
        }
        // Fall back to legacy properties for backward compatibility
        else if (!string.IsNullOrEmpty(data.IconImageName))
        {
#pragma warning disable CS0618 // Type or member is obsolete
            btnData.Image = RevitAddInModularTemplate.Res.ResourceImage.GetImage(data.IconImageName);
            btnData.LargeImage = RevitAddInModularTemplate.Res.ResourceImage.GetImage(data.IconLargeImageName);
            btnData.ToolTipImage = RevitAddInModularTemplate.Res.ResourceImage.GetImage(data.IconLargeImageName);
#pragma warning restore CS0618 // Type or member is obsolete
        }

        btnData.ToolTip = data.Tooltip;
        btnData.LongDescription = data.LongDescription;

        return data.Panel.AddItem(btnData) as PushButton;
    }
}
