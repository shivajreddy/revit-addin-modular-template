using Autodesk.Revit.UI;

namespace Pilot.ui.Revit;

public class RevitSplitButton
{
    private SplitButton _splitButton;
    private RibbonPanel _ribbonPanel;

    public RevitSplitButton(RibbonPanel panel, string name, string text)
    {
        _ribbonPanel = panel;
        var groupData = new SplitButtonData(name, text);
        _splitButton = panel.AddItem(groupData) as SplitButton;
    }

    //private void AddSplitButtonGroup(RibbonPanel panel)
    public void AddRevitPushButton(RevitPushButtonDataModel data)
    {
        // copying following from RevitPushButton.cs
        // Create a name with a guid
        var btnDataName = Guid.NewGuid().ToString();
        // Set the button data
        var btnData = new PushButtonData(btnDataName, data.Label,
            Pilot.core.CoreAssembly.GetCoreAssemblyLocation(), data.CommandNamespacePath)
        {
            LargeImage = Pilot.res.ResourceImage.GetIcon(data.IconImageName),
            ToolTipImage = Pilot.res.ResourceImage.GetIcon(data.IconImageName)
        };

        _splitButton.AddPushButton(btnData);
    }
}
