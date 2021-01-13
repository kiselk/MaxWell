using System;
using System.IO;
using FFImageLoading.Forms;
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


namespace MaxWell.Views.Main
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfileViewPage : ContentPage
    {
        bool isNewItem;

        private ContentPage _tbVisits;

        public ProfileViewPage() { 
        

            InitializeComponent();
            Title = "Profile".Translate();

        }
        public ProfileViewPage(Person person, bool isNew = false)
        {
            InitializeComponent();
            vm = new PersonDetailViewModel(person);
            this.BindingContext = vm;
            isNewItem = isNew;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await personDetailView.PopulateLists();
            await personDetailView.ToggleLists();
        }
      
        PersonDetailViewModel vm;
        async void AddPhoto_Clicked(object sender, EventArgs args)
        {
            //ZZ
            var todoItem = ((PersonDetailViewModel)BindingContext).Person;
            // DisplayAlert("Сохранено", "" , "OK");
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Not Suported", "Your device does not currently support this functionality", "1");
                return;
            }
            try
            {
                var mediaOptions = new PickMediaOptions() { PhotoSize = PhotoSize.Medium };
                var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

                if (selectedImageFile == null)
                {
                    await DisplayAlert("Error", "Empty image", "1");
                    return;
                }

                var memoryStream = new MemoryStream();

                selectedImageFile.GetStream().CopyTo(memoryStream);
                selectedImageFile.Dispose();

                var converted = memoryStream.ToArray();
                //  todoItem.icon = DependencyService.Get<IMediaService>().ResizeImage(converted, 128, 128);
                // todoItem.image = DependencyService.Get<IMediaService>().ResizeImage(converted, 1024, 1024);
                todoItem.image = converted;
                /*   RemoteImage RemoteImage = new RemoteImage();
                  RemoteImage.DownloadedImageBlob = todoItem.image;
                  await App.RemoteImageManager.SaveTaskAsync(RemoteImage, true);
                */
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
                await App.Database2.SaveItemAsync(newPerson);
                await Navigation.PopAsync();
            }
        }

        async void DeleteClicked(object sender, EventArgs args)
        {
            //ZZ
            var todoPerson = ((PersonDetailViewModel)BindingContext).Person;
            //   var todoPerson = (Person)BindingContext;
            await App.Database2.DeleteItemAsync(todoPerson);
            await Navigation.PopAsync();
        }



    }
}