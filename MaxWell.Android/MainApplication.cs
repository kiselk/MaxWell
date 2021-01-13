using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using VKontakte;
using VKontakte.Utils;

namespace MaxWell
{
    [Application]
    public class MainApplication : Application
    {
        public string[] fingerprints;
        public VKAccessToken token;
        public MainApplication(IntPtr handle, JniHandleOwnership transer)
            : base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            VKSdk.Initialize(this).WithPayments();
         
            instance = this;
            fingerprints = VKUtil.GetCertificateFingerprint(this, this.PackageName);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

        }
     

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            UserDialogs.Instance.AlertAsync((e.ExceptionObject as Exception).Message);
        }

         

        private static MainApplication instance;
        public static MainApplication getInstance()
        {
            return instance;
        }
        public bool setToken(VKAccessToken token)
        {
            this.token = token;
            return true;
        }
    }
}