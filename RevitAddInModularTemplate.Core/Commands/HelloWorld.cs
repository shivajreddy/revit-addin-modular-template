using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitAddInModularTemplate.Core.Commands;

// =============================================================================
// NAMESPACE: RevitAddInModularTemplate.Core.Commands
// =============================================================================
// All Revit command implementations. Each command must:
//   1. Implement IExternalCommand
//   2. Have [Transaction] attribute
//   3. Provide GetPath() for registration
// =============================================================================

/// <summary>
/// Example command that displays "Hello, World!". Use as a template for new commands.
/// </summary>
/// <remarks>
/// To create a new command:
/// 1. Copy this file and rename the class
/// 2. Update Execute with your logic
/// 3. Register in InitAddIn.InitializeAddInUI
///
/// Transaction modes: Manual (recommended), Automatic, ReadOnly
/// </remarks>
[Transaction(TransactionMode.Manual)]
public class HelloWorld : IExternalCommand
{
    /// <summary>
    /// Executes when the user clicks the ribbon button.
    /// </summary>
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
        TaskDialog.Show("Hello", "Hello, World!");
        return Result.Succeeded;
    }

    /// <summary>
    /// Gets the fully qualified class name for command registration.
    /// </summary>
    public static string GetPath()
    {
        return typeof(HelloWorld).FullName!;
    }
}
