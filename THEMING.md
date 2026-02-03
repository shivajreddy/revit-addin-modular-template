# Theming System Documentation

This Revit add-in template includes a fully functional automatic theme switching system that responds to Revit's light/dark theme changes.

## Overview

The theming system automatically updates ribbon button icons when users switch between Revit's light and dark themes, providing a polished, professional appearance.

## How It Works

### Architecture

The theming system consists of three main components:

1. **ThemeManager** (`RevitAddInModularTemplate.Res/ThemeManager.cs`)
   - Centralized theme state management
   - Tracks current theme (light/dark)
   - Provides helper methods for loading themed images

2. **RibbonImageThemeSelector** (`RevitAddInModularTemplate/Main.cs`)
   - Subscribes to Revit's `ThemeChanged` event
   - Maintains registry of all theme-aware buttons
   - Automatically updates button images when theme changes

3. **ButtonMetadata** (`RevitAddInModularTemplate.UI/Revit/ButtonMetadata.cs`)
   - Stores button reference and icon base name
   - Enables dynamic image loading during theme switches

### Image Naming Convention

Icons must follow this naming pattern:
```
{basename}-{size}-{theme}.tiff
```

**Examples:**
- `hello-16-dark.tiff` - Small icon (16x16) for dark theme
- `hello-16-light.tiff` - Small icon (16x16) for light theme
- `hello-32-dark.tiff` - Large icon (32x32) for dark theme
- `hello-32-light.tiff` - Large icon (32x32) for light theme

**Size Requirements:**
- Small icons: 16x16 @ 96 DPI
- Large icons: 32x32 @ 192 DPI

## Usage

### Creating Theme-Aware Buttons

#### Method 1: Direct Creation (Recommended)

```csharp
var buttonData = new PushButtonData(
    "MyButton",
    "My\nButton",
    CoreAssembly.GetCoreAssemblyLocation(),
    MyCommand.GetPath()
)
{
    // Use ThemeManager to automatically load correct theme
    Image = ThemeManager.GetSmallIcon("myicon"),
    LargeImage = ThemeManager.GetLargeIcon("myicon"),
    ToolTipImage = ThemeManager.GetLargeIcon("myicon"),
    ToolTip = "My button description"
};

var button = ribbonPanel.AddItem(buttonData) as PushButton;

// Register button for automatic theme updates
ribbonImageThemeSelector.RegisterButton(button, "myicon");
```

#### Method 2: Using RevitPushButtonDataModel

```csharp
var buttonModel = new RevitPushButtonDataModel
{
    Label = "My\nButton",
    Panel = ribbonPanel,
    CommandNamespacePath = MyCommand.GetPath(),
    IconBaseName = "myicon",  // Base name without size/theme suffix
    Tooltip = "My button description",
    LongDescription = "Detailed description of what this button does"
};

var button = RevitPushButton.Create(buttonModel);

// Register for theme updates
ribbonImageThemeSelector.RegisterButton(button, "myicon");
```

### Adding New Themed Icons

1. **Create your icon in both themes:**
   - `myicon-16-dark.tiff` (16x16 @ 96 DPI)
   - `myicon-16-light.tiff` (16x16 @ 96 DPI)
   - `myicon-32-dark.tiff` (32x32 @ 192 DPI)
   - `myicon-32-light.tiff` (32x32 @ 192 DPI)

2. **Add icons to the Res project:**
   - Place files in `RevitAddInModularTemplate.Res/ImageResources/`

3. **Set Build Action:**
   - Right-click each file → Properties → Build Action: `Embedded Resource`
   - Or add to `.csproj`:
     ```xml
     <ItemGroup>
       <EmbeddedResource Include="ImageResources\myicon-16-dark.tiff" />
       <EmbeddedResource Include="ImageResources\myicon-16-light.tiff" />
       <EmbeddedResource Include="ImageResources\myicon-32-dark.tiff" />
       <EmbeddedResource Include="ImageResources\myicon-32-light.tiff" />
     </ItemGroup>
     ```

4. **Use in your code:**
   ```csharp
   Image = ThemeManager.GetSmallIcon("myicon")
   ```

## API Reference

### ThemeManager

**Namespace:** `RevitAddInModularTemplate.Res`

#### Properties

- `CurrentTheme` (string): Current theme name ("light" or "dark")

#### Methods

- `UpdateTheme(ThemeChangedEventArgs args)`: Updates theme based on Revit event
- `GetThemedImage(string baseName, int size)`: Gets themed image for specific size
- `GetSmallIcon(string baseName)`: Gets 16x16 icon for current theme
- `GetLargeIcon(string baseName)`: Gets 32x32 icon for current theme

**Example:**
```csharp
var icon = ThemeManager.GetSmallIcon("hello");  // Returns hello-16-dark.tiff or hello-16-light.tiff
```

### RibbonImageThemeSelector

**Namespace:** `RevitAddInModularTemplate`

#### Methods

- `RegisterButton(RibbonItem button, string iconBaseName)`: Registers a button for automatic theme updates

**Example:**
```csharp
ribbonImageThemeSelector.RegisterButton(myButton, "hello");
```

### ButtonMetadata

**Namespace:** `RevitAddInModularTemplate.UI.Revit`

#### Properties

- `Item` (RibbonItem): The ribbon button/item
- `IconBaseName` (string): Base name of the icon

**Example:**
```csharp
var metadata = new ButtonMetadata(button, "hello");
```

## How Theme Switching Works

1. **Startup:**
   - Default theme is "dark"
   - Buttons load dark-themed icons on initialization
   - `RibbonImageThemeSelector` subscribes to `ThemeChanged` event

2. **Theme Change:**
   - User switches Revit theme (Options → Graphics → Application Theme)
   - Revit fires `ThemeChanged` event
   - `RibbonImageThemeSelector.OnThemeChange()` is called
   - `ThemeManager.UpdateTheme()` updates current theme state
   - `UpdateImages()` iterates through all registered buttons
   - Each button's images are replaced with new theme variants

3. **Result:**
   - All ribbon icons instantly update to match new theme
   - No restart required
   - No user intervention needed

## File Structure

```
RevitAddInModularTemplate.Res/
├── ThemeManager.cs              # Centralized theme management
├── ResourceImage.cs             # Image loading utility
├── ImageResources/              # Icon storage
│   ├── hello-16-dark.tiff
│   ├── hello-16-light.tiff
│   ├── hello-32-dark.tiff
│   └── hello-32-light.tiff

RevitAddInModularTemplate.UI/
└── Revit/
    ├── ButtonMetadata.cs        # Button registration metadata
    ├── RevitPushButton.cs       # Button factory with theme support
    └── RevitPushButtonDataModel.cs  # Button data model

RevitAddInModularTemplate/
└── Main.cs                      # RibbonImageThemeSelector implementation
```

## Backward Compatibility

The system maintains backward compatibility with the old approach:

```csharp
// Old way (still works but shows obsolete warning)
var model = new RevitPushButtonDataModel
{
    IconImageName = "hello-16-dark.tiff",
    IconLargeImageName = "hello-32-dark.tiff"
};

// New way (recommended)
var model = new RevitPushButtonDataModel
{
    IconBaseName = "hello"
};
```

## Troubleshooting

### Images Not Updating

**Problem:** Icons don't change when switching themes

**Solutions:**
1. Verify button is registered: `ribbonImageThemeSelector.RegisterButton(button, "basename")`
2. Check image naming: Must follow `{basename}-{size}-{theme}.tiff` pattern
3. Confirm Build Action: All images must be `Embedded Resource`
4. Check debug output: Look for "Theme changed to: ..." messages

### Missing Images

**Problem:** Icons show as blank or missing

**Solutions:**
1. Verify images exist in `ImageResources/` folder
2. Check Build Action is set to `Embedded Resource`
3. Ensure naming convention is correct
4. Rebuild solution to embed resources

### Wrong Theme on Startup

**Problem:** Dark icons show in light theme on startup

**Solution:**
The current implementation defaults to "dark" theme. To fix this, detect initial theme:

```csharp
// In Main.OnStartup(), after creating ribbonImageThemeSelector:
var currentTheme = revitApplication.ThemeChanged; // Get initial theme
// Update ThemeManager with initial state
```

## Future Enhancements

Planned improvements:

- [ ] Support for `SplitButton` theme switching
- [ ] Support for `PulldownButton` theme switching
- [ ] Support for `ToggleButton` theme switching
- [ ] Automatic theme detection on startup
- [ ] Support for tooltip image theming
- [ ] Caching for performance optimization

## Example: Complete Button Implementation

Here's a complete example showing all components:

```csharp
// In Main.OnStartup()
public Result OnStartup(UIControlledApplication revitApplication)
{
    // Initialize theme selector
    ribbonImageThemeSelector = new RibbonImageThemeSelector(revitApplication);

    // Create ribbon UI
    const string tabName = "My Add-In";
    revitApplication.CreateRibbonTab(tabName);
    ribbonPanel = revitApplication.CreateRibbonPanel(tabName, "Tools");

    // Create themed button
    var myButtonData = new PushButtonData(
        "MyToolButton",
        "My\nTool",
        CoreAssembly.GetCoreAssemblyLocation(),
        MyToolCommand.GetPath()
    )
    {
        Image = ThemeManager.GetSmallIcon("mytool"),
        LargeImage = ThemeManager.GetLargeIcon("mytool"),
        ToolTipImage = ThemeManager.GetLargeIcon("mytool"),
        ToolTip = "Description of my tool"
    };

    var myButton = ribbonPanel.AddItem(myButtonData) as PushButton;

    // Register for theme updates
    ribbonImageThemeSelector.RegisterButton(myButton, "mytool");

    return Result.Succeeded;
}
```

**Required image files:**
- `ImageResources/mytool-16-dark.tiff`
- `ImageResources/mytool-16-light.tiff`
- `ImageResources/mytool-32-dark.tiff`
- `ImageResources/mytool-32-light.tiff`

## Best Practices

1. **Always register buttons** after creation:
   ```csharp
   ribbonImageThemeSelector.RegisterButton(button, "iconname");
   ```

2. **Use consistent base names** across your add-in:
   ```csharp
   // Good
   "export", "import", "settings"
   
   // Avoid
   "ExportButton_Icon", "import_tool_32", "SettingsBtn"
   ```

3. **Create both theme variants** even if they look similar:
   - Don't reuse the same file for both themes
   - Design icons specifically for each theme's background

4. **Test in both themes** before releasing:
   - Switch themes in Revit and verify icons update
   - Check contrast and visibility in both modes

5. **Follow size specifications exactly:**
   - 16x16 @ 96 DPI for small icons
   - 32x32 @ 192 DPI for large icons
   - Use TIFF format for best compatibility

## License

This theming system is part of the RevitAddInModularTemplate and is provided as-is for use in your Revit add-in projects.
