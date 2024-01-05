using CommunityToolkit.Mvvm.Messaging;
using Foundation;
using maui_schedule_slurper.Messages;
using UIKit;

namespace maui_schedule_slurper;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate, IUIApplicationDelegate
{

	protected override MauiApp CreateMauiApp()
	{
		var mauiApp = MauiProgram.CreateMauiApp();

		return mauiApp;
	}
}
