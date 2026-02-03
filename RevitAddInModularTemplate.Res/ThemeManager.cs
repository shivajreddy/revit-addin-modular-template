using Autodesk.Revit.UI.Events;
using System.Windows.Media.Imaging;

namespace RevitAddInModularTemplate.Res;

/// <summary>
/// Centralized theme management for the Revit add-in.
/// Tracks current theme state and provides helper methods for loading themed images.
/// </summary>
public static class ThemeManager
{
    /// <summary>
    /// Current theme name: "light" or "dark"
    /// </summary>
    public static string CurrentTheme { get; private set; } = "dark";

    /// <summary>
    /// Updates the current theme based on Revit's theme change event.
    /// </summary>
    /// <param name="args">Theme change event arguments from Revit</param>
    public static void UpdateTheme(ThemeChangedEventArgs args)
    {
        // Revit's UITheme enum: Light = 0, Dark = 1
        // ThemeChangedEventArgs.ThemeChangedType property returns the new theme
        CurrentTheme = args.ThemeChangedType.ToString().ToLower();
    }

    /// <summary>
    /// Gets a themed image based on the current theme.
    /// </summary>
    /// <param name="baseName">Base name of the image (e.g., "hello")</param>
    /// <param name="size">Icon size: 16 or 32</param>
    /// <returns>BitmapImage for the current theme</returns>
    public static BitmapImage GetThemedImage(string baseName, int size)
    {
        return ResourceImage.GetImage($"{baseName}-{size}-{CurrentTheme}.tiff");
    }

    /// <summary>
    /// Gets the small icon (16x16) for the current theme.
    /// </summary>
    /// <param name="baseName">Base name of the image (e.g., "hello")</param>
    /// <returns>16x16 BitmapImage for the current theme</returns>
    public static BitmapImage GetSmallIcon(string baseName)
    {
        return GetThemedImage(baseName, 16);
    }

    /// <summary>
    /// Gets the large icon (32x32) for the current theme.
    /// </summary>
    /// <param name="baseName">Base name of the image (e.g., "hello")</param>
    /// <returns>32x32 BitmapImage for the current theme</returns>
    public static BitmapImage GetLargeIcon(string baseName)
    {
        return GetThemedImage(baseName, 32);
    }
}
