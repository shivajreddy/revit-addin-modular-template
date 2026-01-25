using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Pilot.core.Commands;

[Transaction(TransactionMode.Manual)]
public class HelloWorld : IExternalCommand
{
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
        TaskDialog.Show("Hello", "Hello World from Pilot!");
        return Result.Succeeded;
    }

    public static string GetPath()
    {
        return typeof(HelloWorld).FullName!;
    }
}
