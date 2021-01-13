using System;
using System.Collections.Generic;
using System.IO;
using Acr.UserDialogs;
using MaxWell.Controls.Dishs;
using MaxWell.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MaxWell.Models;
using MaxWell.Shared.Models;
using MaxWell.Services;
using MaxWell.ViewModels.Dishs;
using ImageCircle.Forms.Plugin.Abstractions;
using MaxWell.Shared.Models.Foods.Plans;
using Plugin.Media;
using Plugin.Media.Abstractions;


namespace MaxWell.Views.Dishs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DishDetailViewPage : ContentPage
    {
        bool isNewItem;

        private ContentPage _dishDetailViewPage;

        public DishDetailViewPage(bool isNew = false)
        {
            InitializeComponent();
            isNewItem = isNew;
            if(isNewItem) Title = "Create".Translate();
            else Title = "Edit".Translate();
        }

        public DishDetailViewPage()
        {
            InitializeComponent();
           Title = "Create".Translate();
            
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
        }

        public DishDetailViewPage(Dish dish, bool isNew = false)
        {
            InitializeComponent();
            vm = new DishDetailViewModel(dish);
            this.BindingContext = vm;
            isNewItem = isNew;
        }

        DishDetailViewModel vm;
        async void DishPhotoClicked(object sender, EventArgs args)
        {

            var dish = ((DishDetailViewModel)BindingContext).Dish;
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

                //  dishDetailView.FindByName<Image>("selectedImage").Source = dish.ImageAsImageStream;
                
            }
            catch (Exception e)
            {
                await DisplayAlert("Error uploading message", "" + e.Message + "|" + e.StackTrace, "OK");
                return;
            }
        }
        
        async void DishSaveClicked(object sender, EventArgs args)
        {
            var dish = ((DishDetailViewModel)BindingContext).Dish;
            if (dish.Name == null)
            {
                DisplayAlert("Ошибка", "Задайте Название", "ОК");
            }
            else
            {
                try
                {
                    await App.DishManager.SaveDishAsync(dish, isNewItem);
                }
                catch (Exception e)
                {
                    UserDialogs.Instance.AlertAsync(e.Message);
                }
                await Navigation.PopAsync();
            }
        }

        async void DishDeleteClicked(object sender, EventArgs args)
        {
            var dish = ((DishDetailViewModel)BindingContext).Dish;
            await App.DishManager.DeleteDishAsync(dish);
            await Navigation.PopAsync();
        }
    }
}