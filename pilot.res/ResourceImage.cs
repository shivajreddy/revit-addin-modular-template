using System.Windows.Media.Imaging;

namespace Pilot.res;

/// <summary>
/// Loads embedded icon images from the pilot.res assembly.
/// Used by <see cref="Pilot.ui.Revit.RevitPushButton"/> to set button icons.
/// </summary>
/// <remarks>
/// <para>
/// Icons must be added to the pilot.res project with the following setup:
/// </para>
/// <list type="bullet">
///   <item>Location: pilot.res/Images/Icons/{filename}.ico</item>
///   <item>Build Action: Embedded Resource (set in file properties)</item>
///   <item>Icon sizes: 16x16 for small, 32x32 for large</item>
/// </list>
/// <para>
/// The full embedded resource path will be: Pilot.res.Images.Icons.{filename}
/// </para>
/// </remarks>
public class ResourceImage
{
    /// <summary>
    /// Loads an icon image from the embedded resources.
    /// </summary>
    /// <param name="name">
    /// The icon filename with extension (e.g., "hello32.ico").
    /// The file must exist at pilot.res/Images/Icons/{name}.
    /// </param>
    /// <returns>
    /// A <see cref="BitmapImage"/> that can be assigned to button Image properties.
    /// Returns an empty image if the resource is not found.
    /// </returns>
    /// <example>
    /// <code>
    /// // Load a 32x32 icon for a button's LargeImage property
    /// var icon = ResourceImage.GetIcon("mycommand32.ico");
    /// </code>
    /// </example>
    public static BitmapImage GetIcon(string name)
    {
        // Build the full resource path: Pilot.res.Images.Icons.{name}
        var resImg = ResourceAssembly.GetNamespace() + "Images.Icons." + name;
        var stream = ResourceAssembly.GetAssembly().GetManifestResourceStream(resImg);

        var img = new BitmapImage();
        img.BeginInit();
        img.StreamSource = stream;
        img.EndInit();

        return img;
    }
}
