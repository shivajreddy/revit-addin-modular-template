using Autodesk.Revit.UI;
using Pilot.ui.Revit;

namespace Pilot;

/// Setup the whole plugins interface -> tabs, panels, buttons
public class SetupInterface
{
	/// Default constructor
	public SetupInterface()
	{
	}

	/// Initialize all the interface elements on custom created Revit Tab
	//public void Initialize(UIControlledApplication app)
	//{
	//	// Create Ribbon Tab
	//	const string tabName = "Pilot-1.0.0";
	//	app.CreateRibbonTab(tabName);
	//}

	public static void Initialize(UIControlledApplication app)
	{
		// Create Ribbon Tab
		const string tabName = "RevitAddInModularTemplate-V1.0.0";
		app.CreateRibbonTab(tabName);
	}
}