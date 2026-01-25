using System.Windows.Media.Imaging;

namespace Pilot.res;

/// <summary>
/// Gets the embedded resource image from the Revtec.res assembly
/// based on the provided file name with extension.
/// </summary>
public class ResourceImage
{

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
