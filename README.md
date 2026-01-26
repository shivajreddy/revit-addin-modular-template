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

### Known Issues

- **Startup project not set** - Visual Studio may not respect the `StartupProject` property in `.slnx` files when opening a new solution. After opening your project for the first time, right-click on the main project (e.g., `MyRevitAddin`) in Solution Explorer and select "Set as Startup Project"

## Roadmap

Upcoming features planned for this template:

- [ ] **PushButton** - Standard push button with icon
- [ ] **SplitButton** - Dropdown button with multiple commands
- [ ] **PulldownButton** - Menu-style button groups
- [ ] **ToggleButton** - On/off state buttons
- [ ] **Button images** - 16x16 and 32x32 icon setup
- [ ] **Tooltip images** - Extended tooltips with GIF previews
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
