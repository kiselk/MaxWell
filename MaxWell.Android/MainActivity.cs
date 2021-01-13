using System;
using System.IO;
using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Net;
using Android.Content.PM;
using Android.Database;
using Android.Graphics;
using Android.Preferences;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using FFImageLoading.Forms.Platform;
using MaxWell.Services;
using ImageCircle.Forms.Plugin.Droid;
using Plugin.CurrentActivity;
using VKontakte;
using VKontakte.Utils;
using Xamarin.Forms;
using Lottie.Forms;
using Lottie.Forms.Droid;
using Plugin.Permissions;
//using Xamarin.Facebook;

namespace MaxWell
{
    [Activity(Label = "MaxWell", Icon = "@drawable/icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal static MainActivity Instance { get; private set; }

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
         //     FacebookSdk.SdkInitialize(ApplicationContext);
            base.OnCreate(bundle);
            Instance = this;
            //  VKSdk.Initialize(this).WithPayments();
            global::Xamarin.Forms.Forms.Init(this, bundle);
            Forms.Init(this, bundle);
            UserDialogs.Init(this);
            CrossCurrentActivity.Current.Init(this, bundle);
            try
            {
                AnimationViewRenderer.Init();

                CachedImageRenderer.Init(true);
              int uiOptions = (int) Window.DecorView.SystemUiVisibility;

            uiOptions |= (int) SystemUiFlags.LowProfile;
            uiOptions |= (int) SystemUiFlags.Fullscreen;
            uiOptions |= (int) SystemUiFlags.HideNavigation;
            uiOptions |= (int) SystemUiFlags.ImmersiveSticky;

            Window.DecorView.SystemUiVisibility = (StatusBarVisibility) uiOptions;
            ImageCircleRenderer.Init();
            
          
            //C1.Android.Core.LicenseManager.Key = License.Key;
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += HandleExceptions;

            }
            catch (Exception e)
            {
                UserDialogs.Instance.Alert(e.Message);
            }
            //====================================
        
            /*
            try
            {
                VKSdk.WakeUpSession(CrossCurrentActivity.Current.Activity);
                ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(CrossCurrentActivity.Current.Activity);
                var email = prefs.GetString("maxwell_vk_email", "none");
                var userid = prefs.GetString("maxwell_vk_id", "none");
                //   mInt = prefs.GetInt("key_for_my_int_value", < default value >);
                //  mString = prefs.GetString("key_for_my_string_value", < default value >);
                var token = VKAccessToken.TokenFromSharedPreferences(CrossCurrentActivity.Current.Activity, "maxwell_vk_token");

                AndroidVkService.Instance.SetUserToken(token);
            }
            catch (Exception ex)
            {
                var x = ex.Message;
            }
            */
            //   AndroidVkService.Instance.WakeUp();
            LoadApplication(new App());
        }

        private void HandleExceptions(object sender, UnhandledExceptionEventArgs e)
        {
            //Handle Exceptions 
        }

     
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public int getOrientation(Android.Net.Uri photoUri)
        {
            ICursor cursor = Application.ApplicationContext.ContentResolver.Query(photoUri, new String[] {MediaStore.Images.ImageColumns.Orientation}, null, null, null);

            if (cursor.Count != 1)
            {
                return -1;
            }

            cursor.MoveToFirst();
            return cursor.GetInt(0);
        }

        protected override async void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            bool vkResult;
            var taska = VKSdk.OnActivityResultAsync(requestCode, resultCode, data, out vkResult);
            if (!vkResult)
            {
                base.OnActivityResult(requestCode, resultCode, data);
         //    AndroidFbService.Instance.OnActivityResult(requestCode, (int)resultCode, data);
             /*  if (requestCode == 1)
                {
                    if (resultCode == Result.Ok)
                    {
                        if (data.Data != null)
                        {
                            //Grab the Uri which is holding the path to the image
                            Android.Net.Uri uri = data.Data;

                            //Read the meta data of the image to determine what orientation the image should be in
                            int orientation = getOrientation(uri);

                            //Stat a background task so we can do all the bitmap stuff off the UI thread
                            BitmapWorkerTask task = new BitmapWorkerTask(this.ContentResolver, uri);
                            task.Execute(orientation);
                        }
                    }
                }
*/
                return;
            }

            try
            {
                var token = await taska;
                token.SaveTokenToSharedPreferences(CrossCurrentActivity.Current.Activity, "maxwell_vk_token");
                ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(CrossCurrentActivity.Current.Activity);
                ISharedPreferencesEditor editor = prefs.Edit();
                if (token.Email != null)
                    if (token.Email != "")
                        editor.PutString("maxwell_vk_email", token.Email);
                    else
                        editor.PutString("maxwell_vk_email", "no email");
                else editor.PutString("maxwell_vk_email", "no email");

                editor.PutString("maxwell_vk_id", token.UserId);

                editor.Commit(); // applies changes synchronously on older APIs
                editor.Apply(); // applies changes asynchronously on newer APIs


                AndroidVkService.Instance.SetUserToken(token);
                MainApplication.getInstance().setToken(token);
                // Get token
            }
            catch (Exception e)
            {
                await UserDialogs.Instance.AlertAsync("Error MainActivity", e.Message); // Handle exception
            }
        }


        public class BitmapWorkerTask : AsyncTask<int, int, Bitmap>
        {
            private Android.Net.Uri uriReference;
            private int data = 0;
            private ContentResolver resolver;

            public BitmapWorkerTask(ContentResolver cr, Android.Net.Uri uri)
            {
                uriReference = uri;
                resolver = cr;
            }

            // Decode image in background.
            protected override Bitmap RunInBackground(params int[] p)
            {
                //This will be the orientation that was passed in from above (task.Execute(orientation);)
                data = p[0];

                Bitmap mBitmap = Android.Provider.MediaStore.Images.Media.GetBitmap(resolver, uriReference);
                Bitmap myBitmap = null;

                if (mBitmap != null)
                {
                    //In order to rotate the image we create a Matrix object, rotate if the image is not already in it's correct orientation
                    Matrix matrix = new Matrix();
                    if (data != 0)
                    {
                        matrix.PreRotate(data);
                    }

                    myBitmap = Bitmap.CreateBitmap(mBitmap, 0, 0, mBitmap.Width, mBitmap.Height, matrix, true);
                    return myBitmap;
                }

                return null;
            }

            //Called when the RunInBackground method has finished
            protected override void OnPostExecute(Bitmap bitmap)
            {
                if (bitmap != null)
                {
                    MemoryStream stream = new MemoryStream();

                    //Compressing by 50%, feel free to change if file size is not a factor
                    bitmap.Compress(Bitmap.CompressFormat.Jpeg, 50, stream);
                    byte[] bitmapData = stream.ToArray();

                    //Send image byte array back to UI
                    MessagingCenter.Send<byte[]>(bitmapData, "ImageSelected");

                    //clean up bitmaps so the app doesn't crash from using up too much memory
                    bitmap.Recycle();
                    GC.Collect();
                }
            }


        
        }
    }
}
