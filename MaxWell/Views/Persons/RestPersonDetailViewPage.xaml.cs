using System;
using System.IO;
using Acr.UserDialogs;
using MaxWell.Controls.Persons;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MaxWell.Models;
using MaxWell.Services;
using MaxWell.ViewModels.Cats;
using MaxWell.ViewModels.Comments;
using MaxWell.ViewModels.Persons;
using MaxWell.ViewModels.Pomets;
using MaxWell.ViewModels.Prides;
using MaxWell.Views.Cats;
using MaxWell.Views.Prides;
using ImageCircle.Forms.Plugin.Abstractions;
using MaxWell.Helpers;
using Plugin.Media;
using Plugin.Media.Abstractions;


namespace MaxWell.Views.Persons
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RestPersonDetailViewPage : ContentPage
    {
        bool isNewItem;

        private ContentPage _tbVisits;

        public RestPersonDetailViewPage(bool isNew = false)
        {
            InitializeComponent();
            isNewItem = isNew;
            if(isNewItem) Title = "Create".Translate();
            else Title = "Edit".Translate();
        }
        public RestPersonDetailViewPage()
        {
            InitializeComponent();
           Title = "Create".Translate();
            
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await personDetailView.PopulateLists();
            await personDetailView.ToggleLists();
        }
        public RestPersonDetailViewPage(Person person, bool isNew = false)
        {
            InitializeComponent();
            vm = new PersonDetailViewModel(person);
            this.BindingContext = vm;
            isNewItem = isNew;
        }
        PersonDetailViewModel vm;

        async void AddPhoto_Clicked(object sender, EventArgs args)
        {
            //ZZ
            var todoItem = ((PersonDetailViewModel) BindingContext).Person;
            // DisplayAlert("Сохранено", "" , "OK");
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Not Suported", "Your device does not currently support this functionality", "1");
                return;
            }
            else if (!(CrossMedia.Current.IsTakePhotoSupported && CrossMedia.Current.IsCameraAvailable))
            {
                await DisplayAlert("Not Suported", "Your device does not currently support this functionality", "1");
                return;
            }

            try
            {
                MediaFile selectedImageFile = null;
                var mediaOptions = new PickMediaOptions() { };
                var cameraOptions = new StoreCameraMediaOptions()
                {
                    SaveToAlbum = true,
                    Directory = "Pictures",
                    Name = $"{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss")}.jpg",

                    CompressionQuality = 75,
                    CustomPhotoSize = 50,
                    PhotoSize = PhotoSize.MaxWidthHeight,
                    MaxWidthHeight = 2000,
                    DefaultCamera = CameraDevice.Front,

                };





                //Ask the user if they want to use the camera or pick from the gallery
                var action = await UserDialogs.Instance.ActionSheetAsync("Добавить Фото", "Отмена", null, null, "Галерея", "Снимок");
                //   var action = await UserDialogs.Instance.Aler ("Add Photo", "Cancel", null, "Choose Existing", "Take Photo");

                if (action == "Галерея")
                {
                    selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);
                }
                else if (action == "Снимок")
                {
                    selectedImageFile = await CrossMedia.Current.TakePhotoAsync(cameraOptions);
                }


                if (selectedImageFile == null)
                {
                    await DisplayAlert("Error", "Empty image", "1");
                    return;
                }

                var memoryStream = new MemoryStream();

                selectedImageFile.GetStream().CopyTo(memoryStream);
                selectedImageFile.Dispose();

                var converted = memoryStream.ToArray();
                todoItem.icon = DependencyService.Get<IMediaService>().ResizeImage(converted, 128, 128);
                todoItem.image = DependencyService.Get<IMediaService>().ResizeImage(converted, 512, 512);

                //    RemoteImage RemoteImage = new RemoteImage();
                //    RemoteImage.DownloadedImageBlob = todoItem.image;
                //    await App.RemoteImageManager.SaveTaskAsync(RemoteImage, true);
                personDetailView.FindByName<Image>("selectedImage").Source = todoItem.ImageAsImageStream;

            }
            catch (Exception e)
            {
                var text = "";

                await DisplayAlert("Error uploading message", "" + e.Message + "|" + e.StackTrace, "OK");
                return;
            }
        }




        async void OnPrideSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem != null)
            {
                PrideListItemViewModel model = (PrideListItemViewModel)args.SelectedItem;

                await Navigation.PushAsync(new PrideDetailViewPage(model.Pride));

            }
        }
        async void ButtonClicked(object sender, EventArgs args)
	    {
            //ZZ
	        var newPerson = ((PersonDetailViewModel)BindingContext).Person;
	        if (newPerson.Name == null)
	        {
	            DisplayAlert("Ошибка", "Задайте имя пользователя", "ОК");
	        }
	        else
	        {
	            await App.PersonManager.SaveTaskAsync(newPerson, isNewItem);
	            await Navigation.PopAsync();
	        }
	    }

        async void DeleteClicked(object sender, EventArgs args)
        {
            //ZZ
            var todoPerson = ((PersonDetailViewModel)BindingContext).Person;
            //   var todoPerson = (Person)BindingContext;
            await App.PersonManager.DeleteTaskAsync(todoPerson);
         
            await Navigation.PopAsync();
        }



    }
}