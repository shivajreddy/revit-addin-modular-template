# Revit Add-In Modular Template

A ready-to-use template for creating Autodesk Revit add-ins using a clean, modular architecture. Features separated projects for UI, business logic, and resources, plus an interactive PowerShell script that scaffolds new projects with proper namespacing and unique GUIDs.

## Quick Start

1. Clone this repository
2. Run the setup script:
   ```powershell
   .\Initialize-Project.ps1
   ```
3. Enter your project name in PascalCase (e.g., `MyRevitAddin`)
4. A new project will be created in a sibling directory

## Getting Started

### Step 1: Prerequisites

Ensure you have the following installed:
- **Visual Studio 2022** (or later) with the ".NET desktop development" workload
- **.NET 8.0 SDK**
- **Revit 2026** installed at the default location (`C:\Program Files\Autodesk\Revit 2026`)
- **PowerShell 5.1+** (included with Windows 10/11)

### Step 2: Clone the Template

```powershell
cd C:\Users\YourName\source\repos
git clone https://github.com/shivajreddy/revit-addin-modular-template.git
cd revit-addin-modular-template
```

### Step 3: Run the Setup Script

```powershell
.\Initialize-Project.ps1
```

The script will prompt you to:
1. Enter your project name in **PascalCase** (e.g., `AcmeTools`, `MyRevitAddin`)
2. Review the changes that will be made
3. Confirm to proceed

The script creates your project in a sibling folder with a **kebab-case** name:
- Input: `MyRevitAddin` → Folder: `my-revit-addin`

### Step 4: Navigate to Your New Project

```powershell
cd ..\my-revit-addin
```

### Step 5: Initialize Git (Optional)

The setup script removes the template's `.git` folder. Initialize a fresh repository:

```powershell
git init
git add .
git commit -m "Initial commit from Revit Add-In Modular Template"
```

### Step 6: Open in Visual Studio

Open the solution file:
```powershell
start MyRevitAddin.slnx
```

Or open Visual Studio and select **File → Open → Project/Solution** and browse to your `.slnx` file.

### Step 7: Build the Solution

In Visual Studio, press `Ctrl+Shift+B` to build. The build automatically copies output files to Revit's Addins folder:
- `%ProgramData%\Autodesk\Revit\Addins\2026\MyRevitAddin\` — DLLs
- `%ProgramData%\Autodesk\Revit\Addins\2026\MyRevitAddin.addin` — manifest

### Step 8: Run and Debug

1. Press `F5` in Visual Studio to start debugging
2. Revit will launch automatically
3. Once Revit opens, find your add-in tab in the ribbon
4. Click your button to test the command
5. Set breakpoints in Visual Studio to debug

### Step 9: Start Developing

| Task | Location |
|------|----------|
| Add new commands | `MyRevitAddin.Core/Commands/` |
| Configure ribbon UI | `MyRevitAddin.UI/` |
| Add icons and images | `MyRevitAddin.Res/` |
| Register external commands | `addin/MyRevitAddin.addin` |

## What the Setup Script Does

The `Initialize-Project.ps1` script guides you through creating a new project:

| Step | Description |
|------|-------------|
| 1 | Choose your project name (PascalCase) |
| 2 | Review changes before proceeding |
| 3 | Copy template to new folder (kebab-case naming) |
| 4 | Remove template-specific files (.git, LICENSE, etc.) |
| 5 | Update all file contents with new name |
| 6 | Rename files and folders |
| 7 | Generate unique add-in GUID |
| 8 | Verify all references updated correctly |

**Example:** If you enter `MyRevitAddin`, the script creates:
```
../my-revit-addin/
  MyRevitAddin/
  MyRevitAddin.Core/
  MyRevitAddin.UI/
  MyRevitAddin.Res/
  MyRevitAddin.slnx
```

## Project Structure

```
ProjectName/              # Main entry point (IExternalApplication)
ProjectName.Core/         # Commands and business logic
ProjectName.UI/           # Ribbon UI configuration
ProjectName.Res/          # Embedded resources (icons, images)
addin/                    # Revit .addin manifest file
lib/                      # Revit API reference DLLs
```

### Why Multiple Projects?

- **Separation of concerns** - UI, logic, and resources are isolated
- **Easier testing** - Core logic can be tested independently
- **Cleaner dependencies** - Each project only references what it needs
- **Scalability** - Add new modules without cluttering the main project

## Design Choices

| Choice | Rationale |
|--------|-----------|
| **PascalCase project names** | Follows C# and .NET naming conventions for namespaces and assemblies |
| **Kebab-case root folder** | Consistent with modern repository naming conventions and URL-friendly |
| **Separate .Res project** | Embedded resources (icons, images) are isolated, making them reusable and keeping binaries out of logic projects |
| **Separate .UI project** | Ribbon configuration is decoupled from commands, allowing UI changes without touching business logic |
| **Separate .Core project** | Commands and business logic can be tested and maintained independently |
| **Local Revit API DLLs** | Stored in `lib/` folder to avoid dependency on installed Revit location and ensure consistent builds |
| **Post-build copy to Addins** | Automatically deploys to Revit's Addins folder for immediate testing |

## Limitations

- **Single Revit version** - Template targets Revit 2026 by default. Future improvement for letting the user choose the revit version
- **No unit test project** - Add your own test project if needed (reference the .Core project)

## Features

### Automatic Theme Switching

The template includes a **fully implemented automatic theme switching system** that responds to Revit's light/dark theme changes:

- **Automatic icon updates** - Ribbon icons instantly update when switching between light/dark themes
- **No user intervention** - Theme changes are detected and handled automatically
- **Simple API** - Easy-to-use ThemeManager for loading themed images
- **Example implementation** - Working example included with Hello World button

**Image Naming Convention:**
```
{iconname}-{size}-{theme}.tiff
Example: hello-16-dark.tiff, hello-32-light.tiff
```

**Usage:**
```csharp
// Load themed icons automatically
Image = ThemeManager.GetSmallIcon("hello"),
LargeImage = ThemeManager.GetLargeIcon("hello"),

// Register for automatic updates
ribbonImageThemeSelector.RegisterButton(button, "hello");
```

For detailed theming documentation, see [THEMING.md](THEMING.md)

## Roadmap

Completed features:
- [x] **PushButton** - Standard push button with icon
- [x] **Button images** - 16x16 and 32x32 icon setup with theme support
- [x] **Automatic theme switching** - Light/dark theme detection and icon updates

Upcoming features planned for this template:

- [ ] **SplitButton theme support** - Theme switching for dropdown buttons
- [ ] **PulldownButton** - Menu-style button groups with theme support
- [ ] **ToggleButton** - On/off state buttons with theme support
- [ ] **Tooltip images** - Extended tooltips with themed GIF previews
- [ ] **Help videos** - F1 help linking to video tutorials
- [ ] **Multi-version support** - Target multiple Revit versions from single codebase

## Requirements

- Visual Studio 2022 or later
- .NET 8.0 SDK
- Revit 2026 (or modify target version in .csproj)

## Building and Debugging

1. Open `ProjectName.slnx` in Visual Studio
2. Build the solution (`Ctrl+Shift+B`)
3. Press `F5` to start debugging with Revit

The build automatically copies the add-in files to Revit's Addins folder.

## License

MIT License - see [LICENSE](LICENSE) file for details
