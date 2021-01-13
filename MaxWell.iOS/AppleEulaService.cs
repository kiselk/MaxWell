using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acr.UserDialogs;
using Foundation;
using MaxWell.iOS;
using MaxWell.Services;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppleEulaService))]

namespace MaxWell.iOS
{
    class AppleEulaService : IEulaService
    {


        public bool IsAccepted()
        {
            try { 
            var plist = NSUserDefaults.StandardUserDefaults;
            if (plist != null)
            {
                return plist.BoolForKey("maxwell_eula");

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
            var plist = NSUserDefaults.StandardUserDefaults;

            plist.SetBool(true, "maxwell_eula");
            plist.Synchronize();


        }
    }
}