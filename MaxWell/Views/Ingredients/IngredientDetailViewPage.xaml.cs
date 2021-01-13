using System;
using System.Collections.Generic;
using System.IO;
using Acr.UserDialogs;
using MaxWell.Controls.Ingredients;
using MaxWell.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MaxWell.Models;
using MaxWell.Shared.Models;
using MaxWell.Services;
using MaxWell.ViewModels.Ingredients;
using ImageCircle.Forms.Plugin.Abstractions;
using MaxWell.Shared.Models.Foods.Plans;
using Plugin.Media;
using Plugin.Media.Abstractions;


namespace MaxWell.Views.Ingredients
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IngredientDetailViewPage : ContentPage
    {
        bool isNewItem;

        private ContentPage _ingredientDetailViewPage;

        public IngredientDetailViewPage(bool isNew = false)
        {
            InitializeComponent();
            isNewItem = isNew;
            if(isNewItem) Title = "Create".Translate();
            else Title = "Edit".Translate();
        }

        public IngredientDetailViewPage()
        {
            InitializeComponent();
           Title = "Create".Translate();
            
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (!isNewItem)

                if (!vm.Ingredient.PersonId.Equals(await App.GetInstance().GetVkUserId()))
            {
                ToolbarItems.Clear();
            }
        }

        public IngredientDetailViewPage(Ingredient ingredient, bool isNew = false)
        {
            InitializeComponent();
            vm = new IngredientDetailViewModel(ingredient);
            this.BindingContext = vm;
            isNewItem = isNew;
        }

        IngredientDetailViewModel vm;
        async void IngredientPhotoClicked(object sender, EventArgs args)
        {

            var ingredient = ((IngredientDetailViewModel)BindingContext).Ingredient;
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
                    
                var action = await UserDialogs.Instance.ActionSheetAsync("AddPhoto".Translate(), "Cancel".Translate(), null, null, "Gallery".Translate(), "Photo".Translate());

                if (action == "Gallery".Translate())
                {
                    selectedImageBytes = await ImageHelper.GetPhotoBytes();
                }
                else if (action == "Photo".Translate())
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

                //  ingredientDetailView.FindByName<Image>("selectedImage").Source = ingredient.ImageAsImageStream;
                
            }
            catch (Exception e)
            {
                await DisplayAlert("Error uploading message", "" + e.Message + "|" + e.StackTrace, "OK");
                return;
            }
        }
        
        async void IngredientSaveClicked(object sender, EventArgs args)
        {
            var ingredient = ((IngredientDetailViewModel)BindingContext).Ingredient;
            if (ingredient.Name == null)
            {
                DisplayAlert("Ошибка", "Задайте Название", "ОК");
            }
            else
            {
                try
                {
                    await App.IngredientManager.SaveIngredientAsync(ingredient, isNewItem);
                }
                catch (Exception e)
                {
                    UserDialogs.Instance.AlertAsync(e.Message);
                }
                await Navigation.PopAsync();
            }
        }

        async void IngredientDeleteClicked(object sender, EventArgs args)
        {
            var ingredient = ((IngredientDetailViewModel)BindingContext).Ingredient;
            await App.IngredientManager.DeleteIngredientAsync(ingredient);
            await Navigation.PopAsync();
        }
    }
}