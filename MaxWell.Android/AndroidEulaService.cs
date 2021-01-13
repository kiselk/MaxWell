using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MaxWell;
using MaxWell.Services;
using Plugin.CurrentActivity;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidEulaService))]
namespace MaxWell
{
    class AndroidEulaService:IEulaService
    {
        public bool IsAccepted()
        {
            try { 

            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(CrossCurrentActivity.Current.Activity);

            

            if (prefs.Contains("maxwell_eula2"))
            {

                return prefs.GetBoolean("maxwell_eula2", false);
              

            }
            }
            catch (Exception e)
            {
                UserDialogs.Instance.AlertAsync(e.Message);
            }
            return false;
        }

        public void Accept()
        {
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(CrossCurrentActivity.Current.Activity);
            ISharedPreferencesEditor editor = prefs.Edit();
          
            editor.PutBoolean("maxwell_eula2", true);

            editor.Commit(); // applies changes synchronously on older APIs
            editor.Apply(); // applies changes asynchronously on newer APIs



        }
    }
}