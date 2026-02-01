using System.Windows.Media.Imaging;

namespace RevitAddInModularTemplate.Res;

/// <summary>
/// Loads embedded icon images for ribbon buttons.
/// </summary>
/// <remarks>
/// Icons must be in Res/Images/Icons/ with Build Action set to "Embedded Resource".
/// Use 16x16(72dpi) for small icons, 32x32(96dpi) for large icons.
/// </remarks>
public class ResourceImage
{
    /// <summary>
    /// Loads an icon from embedded resources.
    /// </summary>
    /// <param name="name">Filename with extension (e.g., "hello32.ico").</param>
    /// <returns>BitmapImage for button Image properties.</returns>
    const string ImageResourcesFolder = "ImageResources";
    public static BitmapImage GetImage(string name)
    {
        //var resImg = ResourceAssembly.GetNamespace() + "Images.Icons." + name;
        var resImg = ResourceAssembly.GetNamespace() + ImageResourcesFolder + "." + name;
        var stream = ResourceAssembly.GetAssembly().GetManifestResourceStream(resImg);

        var img = new BitmapImage();
        img.BeginInit();
        img.StreamSource = stream;
        img.EndInit();

        return img;
    }
}
