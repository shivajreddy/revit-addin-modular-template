using Autodesk.Revit.UI;

namespace RevitAddInModularTemplate.UI.Revit;

/// <summary>
/// Creates split buttons (dropdown with multiple related commands).
/// </summary>
/// <remarks>
/// Usage:
/// 1. Create split button on a panel
/// 2. Add push buttons using <see cref="AddRevitPushButton"/>
/// </remarks>
public class RevitSplitButton
{
    private readonly SplitButton _splitButton;
    private readonly RibbonPanel _ribbonPanel;

    /// <summary>
    /// Creates a split button and adds it to the panel.
    /// </summary>
    /// <param name="panel">Target ribbon panel.</param>
    /// <param name="name">Internal unique name.</param>
    /// <param name="text">Display text (use \n for line break).</param>
    public RevitSplitButton(RibbonPanel panel, string name, string text)
    {
        _ribbonPanel = panel;
        var groupData = new SplitButtonData(name, text);
        _splitButton = panel.AddItem(groupData) as SplitButton;
    }

    /// <summary>
    /// Adds a push button to the split button's dropdown.
    /// </summary>
    public void AddRevitPushButton(RevitPushButtonDataModel data)
    {
        var btnDataName = Guid.NewGuid().ToString();

        var btnData = new PushButtonData(
            btnDataName,
            data.Label,
            RevitAddInModularTemplate.Core.CoreAssembly.GetCoreAssemblyLocation(),
            data.CommandNamespacePath)
        {
            LargeImage = RevitAddInModularTemplate.Res.ResourceImage.GetIcon(data.IconImageName),
            ToolTipImage = RevitAddInModularTemplate.Res.ResourceImage.GetIcon(data.IconImageName)
        };

        _splitButton.AddPushButton(btnData);
    }
}
