using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Acr.UserDialogs;
using FFImageLoading.Forms.Platform;
using Flex;
using MaxWell.iOS;
using UIKit;
using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using Lottie.Forms.iOS.Renderers;
using Syncfusion.SfCalendar.XForms.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using VKontakte;
using VKontakte.API.Methods;

namespace MaxWell
{
	[Register("AppDelegate")]
	public partial class AppDelegate : FormsApplicationDelegate
	{
	    public override bool FinishedLaunching(UIApplication app, NSDictionary options)
	    {
	        Color color = Color.Transparent;
	        // affects all UISwitch controls in the app
	        UISwitch.Appearance.OnTintColor = UIColor.FromRGB(0x91, 0xCA, 0x47);
	        UINavigationBar.Appearance.TintColor = UIColor.White;
	        AppCenter.Start("197e4664-9782-42e4-958a-c96cdd936acb", typeof(Analytics), typeof(Crashes));
	        UIView statusBar = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;
	        statusBar.BackgroundColor = color.ToUIColor();
	        UINavigationBar.Appearance.BarTintColor = color.ToUIColor(); //UIColor.Black);
	        UINavigationBar.Appearance.Translucent = true;
	        UIApplication.SharedApplication.StatusBarHidden = true;
	        /* bool didAppCrash =  Crashes.HasCrashedInLastSessionAsync().Result;
            if (didAppCrash)
            {
                ErrorReport crashReport = Crashes.GetLastSessionCrashReportAsync().Result;
                Crashes..
                    TrackError(crashReport, new Dictionary<string, string> {
                    { "User", Helpers.Settings.UserName },
                    { "Date", DateTime.Now.Date.ToString("MM/dd/yyyy HH:mm tt")},
                    { "AppID", DeviceInfoHelper.AppId },
                    { "someMeaningfulID", id }
                });
            }
            */
	        Forms.Init();
	        try
	        {
	            AnimationViewRenderer.Init();
	            CachedImageRenderer.Init();
	            ImageCircleRenderer.Init();
	            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
	            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
	            FlexButton.Init();
	            new SfCalendarRenderer(); //  C1.Xamarin.Forms.Core.LicenseManager.Key = License.Key;
	            VKSdk.Initialize("6810503");
	            // string a = (AppleVkService.getInstance()).WakeUp().Result;

	            LoadApplication(new App());
	            Facebook.CoreKit.Profile.EnableUpdatesOnAccessTokenChange(true);
	            Facebook.CoreKit.ApplicationDelegate.SharedInstance.FinishedLaunching(app, options);

	        }
	        catch (Exception e)
	        {
	            UserDialogs.Instance.AlertAsync(e.Message);
	        }

	        return base.FinishedLaunching(app, options);
	    }


	    private void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
	    {
	       // throw new NotImplementedException();
	         string z = e.Exception.Message;
	    }

	    private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
	    {
	        string z = e.ExceptionObject.ToString(); // throw new NotImplementedException();
        }
	    public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
	    {
	      //  UserDialogs.Instance.AlertAsync(""+url);

	        return VKSdk.ProcessOpenUrl(url, sourceApplication) || Facebook.CoreKit.ApplicationDelegate.SharedInstance.OpenUrl(application, url, sourceApplication, annotation)

                   || base.OpenUrl(application, url, sourceApplication, annotation);
	    }
        //  [Conditional("DEBUG")]
        private static void DisplayCrashReport()
	    {
	        const string errorFilename = "Fatal.log";
	        var libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Resources);
	        var errorFilePath = Path.Combine(libraryPath, errorFilename);

	        if (!File.Exists(errorFilePath))
	        {
	            return;
	        }

	        var errorText = File.ReadAllText(errorFilePath);
	        var alertView = new UIAlertView("Crash Report", errorText, null, "Close", "Clear") { UserInteractionEnabled = true };
	        alertView.Clicked += (sender, args) =>
	        {
	            if (args.ButtonIndex != 0)
	            {
	                File.Delete(errorFilePath);
	            }
	        };
	        alertView.Show();
	    }



public override void OnActivated(UIApplication application)
        {
            Facebook.CoreKit.AppEvents.ActivateApp();
        }
    }
}