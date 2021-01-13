using System;
using System.Collections.Generic;
using System.IO;
using Acr.UserDialogs;
using MaxWell.Controls.Allergens;
using MaxWell.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MaxWell.Models;
using MaxWell.Services;
using MaxWell.ViewModels.Cats;
using MaxWell.ViewModels.Comments;
using MaxWell.ViewModels.Allergens;
using MaxWell.ViewModels.Pomets;
using MaxWell.ViewModels.Prides;
using MaxWell.Views.Cats;
using MaxWell.Views.Prides;
using ImageCircle.Forms.Plugin.Abstractions;
using MaxWell.Shared.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;


namespace MaxWell.Views.Allergens
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllergenDetailViewPage : ContentPage
    {
        bool isNewItem;

        private ContentPage _allergenDetailViewPage;

        public AllergenDetailViewPage(bool isNew = false)
        {
            InitializeComponent();
            isNewItem = isNew;
            if(isNewItem) Title = "Create".Translate();
            else Title = "Edit".Translate();
        }

        public AllergenDetailViewPage()
        {
            InitializeComponent();
           Title = "Create".Translate();
            
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
          // await allergenDetailView.PopulateLists();
        }

        public AllergenDetailViewPage(Allergen allergen, bool isNew = false)
        {
            InitializeComponent();
            vm = new AllergenDetailViewModel(allergen);
            this.BindingContext = vm;
            isNewItem = isNew;
        }
       
        AllergenDetailViewModel vm;
        async void AllergenPhotoClicked(object sender, EventArgs args)
        {

           

            var allergen = ((AllergenDetailViewModel)BindingContext).Allergen;
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

                //  allergenDetailView.FindByName<Image>("selectedImage").Source = allergen.ImageAsImageStream;
                
            }
            catch (Exception e)
            {
                await DisplayAlert("Error uploading message", "" + e.Message + "|" + e.StackTrace, "OK");
                return;
            }
        }
        
        async void AllergenSaveClicked(object sender, EventArgs args)
        {
            var allergen = ((AllergenDetailViewModel)BindingContext).Allergen;
            if (allergen.Name == null)
            {
                DisplayAlert("Ошибка", "Задайте Название", "ОК");
            }
            else
            {
                try
                {
                    await App.AllergenManager.SaveAllergenAsync(allergen, isNewItem);
                }
                catch (Exception e)
                {
                    UserDialogs.Instance.AlertAsync(e.Message);
                }
                await Navigation.PopAsync();
            }
        }

        async void AllergenDeleteClicked(object sender, EventArgs args)
        {
            var allergen = ((AllergenDetailViewModel)BindingContext).Allergen;
            await App.AllergenManager.DeleteAllergenAsync(allergen);
            await Navigation.PopAsync();
        }
    }
}