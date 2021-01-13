using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MaxWell.Models;
using MaxWell.Services;
using MaxWell.ViewModels.Cats;
using MaxWell.ViewModels.Prides;
using ImageCircle.Forms.Plugin.Abstractions;
using Plugin.Media;
using Plugin.Media.Abstractions;


namespace MaxWell.Views.Prides
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PrideDetailViewPage : ContentPage
    {

       
        public PrideDetailViewPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            vm = (PrideDetailViewModel)BindingContext;


        }
        public PrideDetailViewPage(Pride pride)
        {
            InitializeComponent();
            vm = new PrideDetailViewModel(pride);
            this.BindingContext = vm;
        }
        PrideDetailViewModel vm;
        async void AddPhoto_Clicked(object sender, EventArgs args)
        {
            var todoItem = ((PrideDetailViewModel)BindingContext).Pride;
            // DisplayAlert("Сохранено", "" , "OK");
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await  DisplayAlert("Not Suported","Your device does not currently support this functionalyty", "1");
                return;
            }
            try
            {
                var mediaOptions = new PickMediaOptions() {PhotoSize = PhotoSize.Medium};
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
                todoItem.icon = DependencyService.Get<IMediaService>().ResizeImage(converted, 128, 128);
                todoItem.image = DependencyService.Get<IMediaService>().ResizeImage(converted, 512, 512);

                prideDetailView.FindByName<CircleImage>("selectedImage").Source = todoItem.ImageAsImageStream;

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
	        var newPride = ((PrideDetailViewModel)BindingContext).Pride;
	        if (newPride.Name == null)
	        {
	            DisplayAlert("Ошибка", "Задайте имя прайда", "ОК");
	        }
	        else
	        {
	            await App.Database2.SaveItemAsync(newPride);
	            await Navigation.PopAsync();
	        }
	    }

        async void DeleteClicked(object sender, EventArgs args)
        {
            var todoPride = ((PrideDetailViewModel)BindingContext).Pride;
            await App.Database2.DeleteItemAsync(todoPride);
            await Navigation.PopAsync();
        }

     
    }
}