using System.Windows.Media.Imaging;

namespace RevitAddInModularTemplate.Res;

/// <summary>
/// Loads embedded icon images for ribbon buttons.
/// </summary>
/// <remarks>
/// Icons must be in Res/Images/Icons/ with Build Action set to "Embedded Resource".
/// Use 16x16 for small icons, 32x32 for large icons.
/// </remarks>
public class ResourceImage
{
    /// <summary>
    /// Loads an icon from embedded resources.
    /// </summary>
    /// <param name="name">Filename with extension (e.g., "hello32.ico").</param>
    /// <returns>BitmapImage for button Image properties.</returns>
    public static BitmapImage GetIcon(string name)
    {
        var resImg = ResourceAssembly.GetNamespace() + "Images.Icons." + name;
        var stream = ResourceAssembly.GetAssembly().GetManifestResourceStream(resImg);

        var img = new BitmapImage();
        img.BeginInit();
        img.StreamSource = stream;
        img.EndInit();

        return img;
    }
}
