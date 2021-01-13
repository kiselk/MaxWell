using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Provider;
using Android.Support.V7.Graphics;
using Xamarin.Forms;
using Color = Xamarin.Forms.Color;

[assembly: Dependency(typeof(MaxWell.CameraAndroid))]
namespace MaxWell
{
    public class CameraAndroid : ICameraInterface
    {
        public CameraAndroid()
        {
        }

        public void BringUpCamera()
        {
            var intent = new Intent(MediaStore.ActionImageCapture);
            ((Activity)Forms.Context).StartActivityForResult(intent, 1);
        }

        public void BringUpPhotoGallery()
        {
            var imageIntent = new Intent();
            imageIntent.SetType("image/*");
            imageIntent.SetAction(Intent.ActionGetContent);

            ((Activity)Forms.Context).StartActivityForResult(Intent.CreateChooser(imageIntent, "Select photo"), 1);
        }

       


    }
}