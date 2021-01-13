using System;
using System.Windows.Input;
using MaxWell.Helpers;
using MaxWell.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaxWell.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AboutPage : ContentPage
	{
		public AboutPage ()
		{
			InitializeComponent ();

		    Title = "About".Translate();

		    BindingContext = this;
		   
		}
        protected void OpenWebCommand(object sender, EventArgs e)
	    {
	        Device.OpenUri(new Uri("http://maxwell.vmobile.online"));
	    }
	    protected void OpenWebCommandVM(object sender, EventArgs e)
	    {
	        Device.OpenUri(new Uri("https://vmobile.online/"));
	    }
	    protected void OpenWebCommandGov(object sender, EventArgs e)
	    {
	        Device.OpenUri(new Uri("https://ndb.nal.usda.gov/"));
	    }
	    protected void OpenWebCommandGov2(object sender, EventArgs e)
	    {
	        Device.OpenUri(new Uri("https://ars.usda.gov"));
	    }
	    
    }
}