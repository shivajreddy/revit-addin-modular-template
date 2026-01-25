using Autodesk.Revit.UI;

namespace Pilot;

// Application entry point
public class Main : IExternalApplication
{
    public Result OnStartup(UIControlledApplication currentRevitApplication)
    {
        // Initiate the application using revtec ui
        //var ui = new SetupInterface();
        //ui.Initialize(application);
        SetupInterface.Initialize(currentRevitApplication);

        return Result.Succeeded;
    }

    public Result OnShutdown(UIControlledApplication application)
    {
        return Result.Succeeded;
    }
}
