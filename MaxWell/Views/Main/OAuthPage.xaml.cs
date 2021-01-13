using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MaxWell.Shared.Constants;
using MaxWell.Shared.Models;
using Newtonsoft.Json;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaxWell.Views.Main
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OAuthPage : ContentPage
	{
		public OAuthPage ()
		{
			InitializeComponent ();

		    OAuth2Authenticator auth = new OAuth2Authenticator
		    (
		        clientId: "App ID from https://developers.facebook.com/apps",
		        scope: "",
		        authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
		        redirectUrl: new Uri("http://www.facebook.com/connect/login_success.html"),
		        // switch for new Native UI API
		        //      true = Android Custom Tabs and/or iOS Safari View Controller
		        //      false = Embedded Browsers used (Android WebView, iOS UIWebView)
		        //  default = false  (not using NEW native UI)
		        isUsingNativeUI: true
		    );
        }




	    public async Task RegisterUserAsync(
	        string email, string password, string confirmPassword)
	    {
	        var model = new RegisterBindingModel
	        {
	            Email = email,
	            Password = password,
	            ConfirmPassword = confirmPassword
	        };
	        var json = JsonConvert.SerializeObject(model);
	        HttpContent httpContent = new StringContent(json);
	        httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
	        var client = new HttpClient();
	        var response = await client.PostAsync(
	           AccountConstants.url, httpContent);
	    }


    }
}