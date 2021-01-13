using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using FFImageLoading.Forms;
using MaxWell.Helpers;
using MaxWell.Models;
using MaxWell.ViewModels.Foods;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace MaxWell.Services
{
    class CameraService
    {

        private CameraService()
        {

        }

        private static CameraService Instance;

        public static CameraService GetInstance()
        {
            if (Instance == null) Instance = new CameraService();
            return Instance;
        }


        public async Task<string> UploadPhoto()
        {
            byte[] image = await this.GetImageBytes();
            RemoteImage RemoteImage = new RemoteImage();
            RemoteImage.DownloadedImageBlob = image;
            RemoteImage = await App.RemoteImageManager.SaveTaskAsync(RemoteImage, true);
            return RemoteImage.ImageUrl;
        }
        public async Task<byte[]> GetImageBytes()
        {
            //ZZ
           
            // DisplayAlert("Сохранено", "" , "OK");
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await UserDialogs.Instance.AlertAsync("Not Suported", "Your device does not currently support this functionality", "1");
                return null;
            }
            else if (!(CrossMedia.Current.IsTakePhotoSupported && CrossMedia.Current.IsCameraAvailable))
            {
                await UserDialogs.Instance.AlertAsync("Not Suported", "Your device does not currently support this functionality", "1");
                return null;
            }
            try
            {
                var action = await UserDialogs.Instance.ActionSheetAsync("Добавить Фото", "Отмена", null, null, "Галерея", "Снимок");
                if (action == "Галерея") { 
                    return await ImageHelper.GetPhotoBytes();
                }
                else if (action == "Снимок")
                {
                return await ImageHelper.GetCameraBytes();
                }

              

            }
            catch (Exception e)
            {
                var text = "";

                await UserDialogs.Instance.AlertAsync("Error uploading message", "" + e.Message + "|" + e.StackTrace, "OK");
               
            }
            return null;
        }






    }
}
