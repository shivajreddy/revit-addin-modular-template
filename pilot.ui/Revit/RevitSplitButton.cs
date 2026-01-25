using Autodesk.Revit.UI;

namespace Pilot.ui.Revit;

/// <summary>
/// Wrapper class for creating and managing Revit split buttons.
/// A split button displays a dropdown with multiple related commands.
/// </summary>
/// <remarks>
/// <para>
/// Split buttons are useful for grouping related commands. The top/visible
/// button shows the last-used command, and clicking the dropdown arrow
/// reveals all available options.
/// </para>
/// <para>
/// Usage pattern:
/// </para>
/// <list type="number">
///   <item>Create the split button on a panel</item>
///   <item>Add push buttons to it using <see cref="AddRevitPushButton"/></item>
/// </list>
/// </remarks>
/// <example>
/// <code>
/// var splitBtn = new RevitSplitButton(panel, "ViewTools", "View\nTools");
/// splitBtn.AddRevitPushButton(new RevitPushButtonDataModel { ... });
/// splitBtn.AddRevitPushButton(new RevitPushButtonDataModel { ... });
/// </code>
/// </example>
public class RevitSplitButton
{
    private readonly SplitButton _splitButton;
    private readonly RibbonPanel _ribbonPanel;

    /// <summary>
    /// Creates a new split button and adds it to the specified panel.
    /// </summary>
    /// <param name="panel">The ribbon panel where the split button will be added.</param>
    /// <param name="name">Internal unique name for the split button.</param>
    /// <param name="text">Display text shown on the button (use \n for line break).</param>
    public RevitSplitButton(RibbonPanel panel, string name, string text)
    {
        _ribbonPanel = panel;
        var groupData = new SplitButtonData(name, text);
        _splitButton = panel.AddItem(groupData) as SplitButton;
    }

    /// <summary>
    /// Adds a push button to this split button's dropdown menu.
    /// </summary>
    /// <param name="data">
    /// The button configuration. Only Label, CommandNamespacePath, and IconImageName
    /// are used (Panel property is ignored since button is added to the split button).
    /// </param>
    public void AddRevitPushButton(RevitPushButtonDataModel data)
    {
        // Use GUID to ensure unique internal button name
        var btnDataName = Guid.NewGuid().ToString();

        var btnData = new PushButtonData(
            btnDataName,
            data.Label,
            Pilot.core.CoreAssembly.GetCoreAssemblyLocation(),
            data.CommandNamespacePath)
        {
            LargeImage = Pilot.res.ResourceImage.GetIcon(data.IconImageName),
            ToolTipImage = Pilot.res.ResourceImage.GetIcon(data.IconImageName)
        };

        _splitButton.AddPushButton(btnData);
    }
}
