using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using FFImageLoading.Forms;
using MaxWell.Controls.Foods;
using MaxWell.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MaxWell.Models;
using MaxWell.Services;
using MaxWell.ViewModels.Cats;
using MaxWell.ViewModels.Comments;
using MaxWell.ViewModels.Foods;
using MaxWell.ViewModels.Pomets;
using MaxWell.ViewModels.Prides;
using MaxWell.Views.Cats;
using MaxWell.Views.Prides;
using ImageCircle.Forms.Plugin.Abstractions;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.TextToSpeech;

namespace MaxWell.Views.Foods
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FoodDetailViewPage : ContentPage
    {
        bool isNewItem;

        private ContentPage _tbVisits;

        public FoodDetailViewPage(bool isNew = false)
        {
            InitializeComponent();
            isNewItem = isNew;
            if(isNewItem) Title = "Create".Translate(); 
            else Title = "Edit".Translate();
        }

        public FoodDetailViewPage()
        {
            InitializeComponent();
           Title = "Create".Translate();

        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await foodDetailView.PopulateLists();
            //  foodDetailView.DoFocus();
            if(!isNewItem)
            if (!(Convert.ToInt32(vm.Food.VKUserId)).Equals(await App.GetInstance().GetVkUserId()))
            {
                ToolbarItems.Clear();
            }

        }
        public FoodDetailViewPage(Food food, bool isNew = false)
        {
            InitializeComponent();
            vm = new FoodDetailViewModel(food);
            this.BindingContext = vm;
            isNewItem = isNew;
        }
        FoodDetailViewModel vm;


        async void AddPhoto_Clicked(object sender, EventArgs args)
        {
            //ZZ
            var todoItem= ((FoodDetailViewModel)BindingContext).Food;
            // DisplayAlert("Сохранено", "" , "OK");
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
                    
               


              
                    //Ask the user if they want to use the camera or pick from the gallery
                    var action = await UserDialogs.Instance.ActionSheetAsync("Добавить Фото", "Отмена", null, null, "Галерея", "Снимок");
                    //   var action = await UserDialogs.Instance.Aler ("Add Photo", "Cancel", null, "Choose Existing", "Take Photo");

                    if (action == "Галерея")
                    {
                      //   selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);
                         selectedImageBytes = await ImageHelper.GetPhotoBytes();
                    }
                    else if (action == "Снимок")
                    {
                        // selectedImageFile = await CrossMedia.Current.TakePhotoAsync(cameraOptions);
                         selectedImageBytes = await ImageHelper.GetCameraBytes();
                    }

               // selectedImageBytes = ImageHelper.MakeTransparentBackground(selectedImageBytes);


                if (selectedImageBytes == null)
                {
                    await DisplayAlert("Error", "Empty image", "1");
                    return;
                }
                /*
                var memoryStream = new MemoryStream();

                selectedImageFile.GetStream().CopyTo(memoryStream);
                selectedImageFile.Dispose();

                var converted = memoryStream.ToArray();
           */  //   todoItem.icon = DependencyService.Get<IMediaService>().ResizeImage(converted, 128, 128);
               // todoItem.image = DependencyService.Get<IMediaService>().ResizeImage(selectedImageBytes, 512, 512);
                todoItem.image = selectedImageBytes;
            //    RemoteImage RemoteImage = new RemoteImage();
            //    RemoteImage.DownloadedImageBlob = todoItem.image;
            //    await App.RemoteImageManager.SaveTaskAsync(RemoteImage, true);
                foodDetailView.FindByName<CachedImage>("selectedImage").Source = todoItem.ImageAsImageStream;
         
            }
            catch (Exception e)
            {
                var text = "";
                
                await DisplayAlert("Error uploading message", "" + e.Message + "|" + e.StackTrace, "OK");
                return;
            }
        }




      
        async void ButtonClicked(object sender, EventArgs args)
	    {
            //ZZ
	        var newFood = ((FoodDetailViewModel)BindingContext).Food;
	        if (newFood.Name == null)
	        {
	            DisplayAlert("Ошибка", "Задайте Название", "ОК");
	        }
	        else
	        {
	          

	            try
	            {
	                await App.FoodManager.SaveFoodAsync(newFood, isNewItem);
	            }
                catch (Exception e)
	            {
	                UserDialogs.Instance.AlertAsync(e.Message);
	            }

	            await Navigation.PopAsync();
	        }
	    }

        async void DeleteClicked(object sender, EventArgs args)
        {
            //ZZ
            var todoFood = ((FoodDetailViewModel)BindingContext).Food;
            //   var todoFood = (Food)BindingContext;
            await App.FoodManager.DeleteFoodAsync(todoFood);
         
            await Navigation.PopAsync();
        }

        public async Task SpeakInfo()
        {
          await  foodDetailView.Speak();
        }
        async void SpeakButtonClicked(object sender, EventArgs args)
        {
            //ZZZ
            var todoFood = ((FoodDetailViewModel)BindingContext).Food;
            await VoiceService.getInstance().SpeakRu(todoFood.Name);
            SpeakInfo();
            // await VoiceService.getInstance().SpeakEn(todoFood.NameEn);

            /*  var languagesRu = await CrossTextToSpeech.Current.GetInstalledLanguages();
              var languagesEn = await CrossTextToSpeech.Current.GetInstalledLanguages();
              try
              {
                  var languageRu = languagesRu.FirstOrDefault(l => l.Language == "ru-RU");
                  CrossTextToSpeech.Current.Speak(todoFood.Name, languageRu);
                  foodDetailView.Speak();
               }
              catch (Exception e)
              {
                  UserDialogs.Instance.AlertAsync(e.Message); }
              try
              {
                  var languageEn = languagesEn.FirstOrDefault(l => l.Language == "en-US");
                  CrossTextToSpeech.Current.Speak(todoFood.NameEn, languageEn);
              }
              catch (Exception e) { UserDialogs.Instance.AlertAsync(e.Message); }
             */
        }

        async void TakePhoto(object sender, EventArgs args_)
        {
            foodDetailView.TakePhoto(sender, args_);
        }

    }
}