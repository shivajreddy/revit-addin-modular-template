# Revit Add-In Modular Template

A template for creating Revit add-ins with a modular project structure. Clone this template and run the setup script to create your own Revit add-in project.

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
