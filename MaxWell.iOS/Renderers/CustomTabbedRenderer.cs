using System;
using MaxWell.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(TabbedPage), typeof(CustomTabbedRenderer))]
namespace MaxWell.iOS.Renderers
{
    public class CustomTabbedRenderer : TabbedRenderer
    {
        public override UIViewController SelectedViewController
        {
            get
            {
                return base.SelectedViewController;
            }
            set
            {
                base.SelectedViewController = value;

                if (SelectedViewController.GetType() == typeof(UINavigationController))
                {
                    (SelectedViewController as UINavigationController).NavigationBarHidden = true;
                }
            }
        }
    }
}
