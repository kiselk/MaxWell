using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
//using ImageMagick;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;

namespace MaxWell.Helpers
{
    class ImageHelper
    {
        public static byte[] ImageUrlToByteArray(string source)
        {
            
             using (var webClient = new WebClient())
            {
                byte[] imageBytes = webClient.DownloadData(source);
               return imageBytes;
            }

       

         
            }
  /*
        public static byte[] ImageUrlToByteArrayTransparent(string source)
        {
            return MakeTransparentBackground(ImageUrlToByteArray(source));
        }

        public static ImageSource ImageUrlToByteStreamTransparent(string source)
        {
          return ImageSource.FromStream(() => new MemoryStream(MakeTransparentBackground(ImageUrlToByteArray(source))));
       
        }
     
    public static byte[] MakeTransparentBackground(byte[] source)
    {
      //  byte[] output = source;
            using (var img = new MagickImage(source))//"image.jpg"))
            {
                // -fuzz XX%
                img.ColorFuzz = new Percentage(10);
                // -transparent white
                img.Transparent(MagickColors.White);
                return img.ToByteArray();
            }

        return source;

    }
    */


      
            /// <summary>
            /// Returns Photo Bytes.
            /// </summary>
            /// <returns>Photo Byte Array.</returns>
            public static async Task<byte[]> GetCameraBytes()
            {
                var photo = await GetCameraStream();
                var memoryStream = new MemoryStream();

                photo?.CopyTo(memoryStream);
              

                return memoryStream.ToArray();
            }
        public static async Task<byte[]> GetPhotoBytes()
        {
            var photo = await GetPhotoStream();
            var memoryStream = new MemoryStream();

            photo?.CopyTo(memoryStream);


            return memoryStream.ToArray();
        }
        /// <summary>
        /// Photo Stream.
        /// </summary>
        /// <returns>Photo Stream.</returns>
        public static async Task<Stream> GetCameraStream()
        {
            //MediaFile file = null;
            await CrossMedia.Current.Initialize();
                try
                {
                    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                    {
                        await Application.Current.MainPage.DisplayAlert("No Camera", "No camera available.", "OK");
                        return null;
                    }

                    var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                    if (cameraStatus != PermissionStatus.Granted)
                    {
                        var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] {Permission.Camera});
                        cameraStatus = results[Permission.Camera];
                    }

                    if (cameraStatus == PermissionStatus.Granted)
                    {
                      var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                        {
    
                            RotateImage = true,
                            AllowCropping = true,
                            Name = "photo.jpg",
                            SaveToAlbum = false,
                            DefaultCamera = CameraDevice.Rear
    
    
    
                       });     /* 
                       // Device.BeginInvokeOnMainThread(async () =>
                      //  {
                            file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                            {
                                PhotoSize = PhotoSize.Medium, Directory = "Receipts", Name = $"{DateTime.UtcNow}.jpg"
                            });

                           
                       // }); */
                    return file?.GetStream();
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Permissions Denied", "Unable to take photos.", "OK");

                        if (Device.RuntimePlatform == Device.iOS)
                        {
                            var result = await Application.Current.MainPage.DisplayAlert("Settings", "Update camera permissions?", "Yes", "No");
                            if (result)
                            {
                                CrossPermissions.Current.OpenAppSettings();
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    UserDialogs.Instance.AlertAsync(e.Message);
                }

                return null;
            }


        public static async Task<Stream> GetPhotoStream()
        {
            await CrossMedia.Current.Initialize();
             var mediaOptions = new PickMediaOptions() { };
            MediaFile file = (await CrossMedia.Current.PickPhotoAsync(mediaOptions));
            return file?.GetStreamWithImageRotatedForExternalStorage();
        }
    }


}
