using Autodesk.Revit.UI;

namespace Pilot.ui.Revit;

/// <summary>
/// Factory class for creating Revit ribbon push buttons with icons.
/// </summary>
/// <remarks>
/// <para>
/// Use this class when you need buttons with custom icons. For simple buttons
/// without icons, you can create <see cref="PushButtonData"/> directly in
/// <see cref="Pilot.InitAddIn"/>.
/// </para>
/// <para>
/// Icons must be embedded resources in the pilot.res project under Images/Icons/.
/// </para>
/// </remarks>
public class RevitPushButton
{
    /// <summary>
    /// Creates and adds a push button to a ribbon panel with full icon support.
    /// </summary>
    /// <param name="data">
    /// The button configuration including label, command, and icon file names.
    /// See <see cref="RevitPushButtonDataModel"/> for required properties.
    /// </param>
    /// <returns>
    /// The created <see cref="PushButton"/> instance, allowing further customization.
    /// </returns>
    /// <remarks>
    /// The button is automatically added to the panel specified in the data model.
    /// A GUID is used as the internal button name to ensure uniqueness.
    /// </remarks>
    /// <example>
    /// <code>
    /// var data = new RevitPushButtonDataModel
    /// {
    ///     Label = "Hello\nWorld",
    ///     Panel = panel,
    ///     CommandNamespacePath = HelloWorld.GetPath(),
    ///     Tooltip = "Shows a greeting",
    ///     IconImageName = "hello16.ico",
    ///     IconLargeImageName = "hello32.ico"
    /// };
    /// var button = RevitPushButton.Create(data);
    /// </code>
    /// </example>
    public static PushButton Create(RevitPushButtonDataModel data)
    {
        // Use GUID to ensure unique internal button name
        var btnDataName = Guid.NewGuid().ToString();

        // Create button data with command registration
        var btnData = new PushButtonData(
            btnDataName,
            data.Label,
            Pilot.core.CoreAssembly.GetCoreAssemblyLocation(),
            data.CommandNamespacePath)
        {
            // Small icon for compact display (16x16 pixels)
            Image = Pilot.res.ResourceImage.GetIcon(data.IconImageName),
            // Large icon for standard display (32x32 pixels)
            LargeImage = Pilot.res.ResourceImage.GetIcon(data.IconLargeImageName),
            // Tooltip image (typically same as large icon)
            ToolTipImage = Pilot.res.ResourceImage.GetIcon(data.IconLargeImageName),

            ToolTip = data.Tooltip,
            LongDescription = data.LongDescription,
        };

        // Optional: Set availability class to control when button is enabled
        // btnData.AvailabilityClassName = "Pilot.ui.Revit.CustomAvailability";

        return data.Panel.AddItem(btnData) as PushButton;
    }
}

