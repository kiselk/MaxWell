using System;
using System.Collections.Generic;
using System.IO;
using Acr.UserDialogs;
using MaxWell.Controls.Meals;
using MaxWell.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MaxWell.Models;
using MaxWell.Shared.Models;
using MaxWell.Services;
using MaxWell.ViewModels.Meals;
using ImageCircle.Forms.Plugin.Abstractions;
using MaxWell.Shared.Models.Foods.Plans;
using Plugin.Media;
using Plugin.Media.Abstractions;


namespace MaxWell.Views.Meals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MealDetailViewPage : ContentPage
    {
        bool isNewItem;

        private ContentPage _mealDetailViewPage;

        public MealDetailViewPage(bool isNew = false)
        {
            InitializeComponent();
            isNewItem = isNew;
            if(isNewItem) Title = "Create".Translate();
            else Title = "Edit".Translate();
        }

        public MealDetailViewPage()
        {
            InitializeComponent();
           Title = "Create".Translate();
            
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
        }

        public MealDetailViewPage(Meal meal, bool isNew = false)
        {
            InitializeComponent();
            vm = new MealDetailViewModel(meal);
            this.BindingContext = vm;
            isNewItem = isNew;
        }

        MealDetailViewModel vm;
        async void MealPhotoClicked(object sender, EventArgs args)
        {

            var meal = ((MealDetailViewModel)BindingContext).Meal;
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

                //  mealDetailView.FindByName<Image>("selectedImage").Source = meal.ImageAsImageStream;
                
            }
            catch (Exception e)
            {
                await DisplayAlert("Error uploading message", "" + e.Message + "|" + e.StackTrace, "OK");
                return;
            }
        }
        
        async void MealSaveClicked(object sender, EventArgs args)
        {
            var meal = ((MealDetailViewModel)BindingContext).Meal;
            if (meal.Name == null)
            {
                DisplayAlert("Ошибка", "Задайте Название", "ОК");
            }
            else
            {
                try
                {
                    await App.MealManager.SaveMealAsync(meal, isNewItem);
                }
                catch (Exception e)
                {
                    UserDialogs.Instance.AlertAsync(e.Message);
                }
                await Navigation.PopAsync();
            }
        }

        async void MealDeleteClicked(object sender, EventArgs args)
        {
            var meal = ((MealDetailViewModel)BindingContext).Meal;
            await App.MealManager.DeleteMealAsync(meal);
            await Navigation.PopAsync();
        }
    }
}