using System;
using Acr.UserDialogs;
using BottomBar.XamarinForms;
using MaxWell.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaxWell.Views.Main
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : BottomBarPage
	{
		public MainPage ()
		{
			InitializeComponent ();
		  // this.IgnoresContainerArea = true;
            
		}

	    protected override async void OnAppearing()
	    {
	        try
	        {
	            if (!DependencyService.Get<IEulaService>().IsAccepted())
	            {
	                Navigation.PushModalAsync(new Eula());
	            }
	        }
	        catch (Exception e)
	        {
	            UserDialogs.Instance.AlertAsync(e.Message);
	        }
	    }
	}
}