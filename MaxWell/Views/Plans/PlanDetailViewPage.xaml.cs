using System;
using System.Collections.Generic;
using System.IO;
using Acr.UserDialogs;
using MaxWell.Controls.Plans;
using MaxWell.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MaxWell.Models;
using MaxWell.Shared.Models;
using MaxWell.Services;
using MaxWell.ViewModels.Plans;
using ImageCircle.Forms.Plugin.Abstractions;
using MaxWell.Shared.Models.Foods.Plans;
using Plugin.Media;
using Plugin.Media.Abstractions;


namespace MaxWell.Views.Plans
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlanDetailViewPage : ContentPage
    {
        bool isNewItem;

        private ContentPage _planDetailViewPage;

        public PlanDetailViewPage(bool isNew = false)
        {
            InitializeComponent();
            isNewItem = isNew;
            if(isNewItem) Title = "Create".Translate();
            else Title = "Edit".Translate();
        }

        public PlanDetailViewPage()
        {
            InitializeComponent();
           Title = "Create".Translate();
            
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
        }

        public PlanDetailViewPage(Plan plan, bool isNew = false)
        {
            InitializeComponent();
            vm = new PlanDetailViewModel(plan);
            this.BindingContext = vm;
            isNewItem = isNew;
        }

        PlanDetailViewModel vm;
        async void PlanPhotoClicked(object sender, EventArgs args)
        {

            var plan = ((PlanDetailViewModel)BindingContext).Plan;
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await  DisplayAlert("Not Suported","Your device does not currently support this functionality", "1");
                return;
            } else if (! (CrossMedia.Current.IsTakePhotoSupported && CrossMedia.Current.IsCameraAvailable))
            {
                await DisplayAlert("Not Suported", "Your device does not currently support this functionality", "1");
                return;
            }
            try
            {
                MediaFile selectedImageFile = null;
                byte[] selectedImageBytes = null;
                var mediaOptions = new PickMediaOptions() {};
                var cameraOptions = new StoreCameraMediaOptions() {
                    SaveToAlbum = true,
                    Directory = "Pictures",
                    Name = $"{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss")}.jpg",

                    CompressionQuality = 75,
                    CustomPhotoSize = 50,
                    PhotoSize = PhotoSize.MaxWidthHeight,
                    MaxWidthHeight = 1000,
                    DefaultCamera = CameraDevice.Front,
                   
                };
                    
                var action = await UserDialogs.Instance.ActionSheetAsync("Добавить Фото", "Отмена", null, null, "Галерея", "Снимок");

                if (action == "Галерея")
                {
                    selectedImageBytes = await ImageHelper.GetPhotoBytes();
                }
                else if (action == "Снимок")
                {
                    selectedImageBytes = await ImageHelper.GetCameraBytes();
                }

                if (selectedImageBytes == null)
                {
                    await DisplayAlert("Error", "Empty image", "1");
                    return;
                }
                // todoItem.image = DependencyService.Get<IMediaService>().ResizeImage(selectedImageBytes, 512, 512);

                //  RemoteImage RemoteImage = new RemoteImage();
                //  RemoteImage.DownloadedImageBlob = todoItem.image;
                //  await App.RemoteImageManager.SaveTaskAsync(RemoteImage, true);

                //  planDetailView.FindByName<Image>("selectedImage").Source = plan.ImageAsImageStream;
                
            }
            catch (Exception e)
            {
                await DisplayAlert("Error uploading message", "" + e.Message + "|" + e.StackTrace, "OK");
                return;
            }
        }
        
        async void PlanSaveClicked(object sender, EventArgs args)
        {
            var plan = ((PlanDetailViewModel)BindingContext).Plan;
            if (plan.Name == null)
            {
                DisplayAlert("Ошибка", "Задайте Название", "ОК");
            }
            else
            {
                try
                {
                    await App.PlanManager.SavePlanAsync(plan, isNewItem);
                }
                catch (Exception e)
                {
                    UserDialogs.Instance.AlertAsync(e.Message);
                }
                await Navigation.PopAsync();
            }
        }

        async void PlanDeleteClicked(object sender, EventArgs args)
        {
            var plan = ((PlanDetailViewModel)BindingContext).Plan;
            await App.PlanManager.DeletePlanAsync(plan);
            await Navigation.PopAsync();
        }
    }
}