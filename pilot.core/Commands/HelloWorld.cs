using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Pilot.core.Commands;

// =============================================================================
// NAMESPACE: Pilot.core.Commands
// =============================================================================
// Contains all Revit command implementations. Each command must:
//   1. Implement IExternalCommand interface
//   2. Have the [Transaction] attribute specifying transaction mode
//   3. Provide a GetPath() method returning the full class name for registration
//
// Commands are invoked when users click buttons in the Revit ribbon.
// =============================================================================

/// <summary>
/// A simple example command that displays a "Hello, World!" message dialog.
/// Use this as a template for creating new commands.
/// </summary>
/// <remarks>
/// <para>
/// To create a new command:
/// </para>
/// <list type="number">
///   <item>Copy this file and rename the class</item>
///   <item>Update the Execute method with your logic</item>
///   <item>Register the command in <see cref="Pilot.InitAddIn.InitializeAddInUI"/></item>
/// </list>
/// <para>
/// Transaction modes:
/// </para>
/// <list type="bullet">
///   <item><c>Manual</c>: You control transactions explicitly (recommended)</item>
///   <item><c>Automatic</c>: Revit wraps Execute in a transaction automatically</item>
///   <item><c>ReadOnly</c>: No modifications allowed to the document</item>
/// </list>
/// </remarks>
[Transaction(TransactionMode.Manual)]
public class HelloWorld : IExternalCommand
{
    /// <summary>
    /// Executes the command when the user clicks the associated ribbon button.
    /// </summary>
    /// <param name="commandData">
    /// Provides access to the Revit application, active document, and view.
    /// </param>
    /// <param name="message">
    /// Set this to display an error message if returning <see cref="Result.Failed"/>.
    /// </param>
    /// <param name="elements">
    /// Highlight these elements if returning <see cref="Result.Failed"/>.
    /// </param>
    /// <returns>
    /// <see cref="Result.Succeeded"/> on success,
    /// <see cref="Result.Failed"/> on error,
    /// <see cref="Result.Cancelled"/> if the user cancels.
    /// </returns>
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
        TaskDialog.Show("Hello", "Hello, World!");
        return Result.Succeeded;
    }

    /// <summary>
    /// Gets the fully qualified class name for command registration.
    /// </summary>
    /// <returns>The full namespace path to this command class.</returns>
    /// <example>
    /// Returns: "Pilot.core.Commands.HelloWorld"
    /// </example>
    public static string GetPath()
    {
        return typeof(HelloWorld).FullName!;
    }
}
