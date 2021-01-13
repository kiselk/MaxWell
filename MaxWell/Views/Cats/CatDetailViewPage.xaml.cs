using System;
using System.IO;
using Acr.UserDialogs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MaxWell.Models;
using MaxWell.Services;
using MaxWell.ViewModels.Cats;
using Plugin.Media;
using Plugin.Media.Abstractions;


namespace MaxWell.Views.Cats
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CatDetailViewPage : ContentPage
    {

        CatDetailViewModel vm;
        private ContentPage _tbVisits;

        public CatDetailViewPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            ((App)App.Current).ResumeAtImageId = -1;
            var cat = (Cat)BindingContext;
            if(cat==null)
            {
                this.FindByName<Button>("PregnantButton").IsVisible=false;
                this.FindByName<Button>("BirthButton").IsVisible = false;
                this.FindByName<Button>("BreedButton").IsVisible = false;
            }
            else
            {
                if(cat.Gender==null)
                {
                    this.FindByName<Button>("PregnantButton").IsVisible = false;
                    this.FindByName<Button>("BirthButton").IsVisible = false;
                    this.FindByName<Button>("BreedButton").IsVisible = false;
                }
                else
                if (!cat.Gender.Equals("Девочка"))
                {
                    this.FindByName<Button>("PregnantButton").IsVisible = false;
                    this.FindByName<Button>("BirthButton").IsVisible = false;
                    this.FindByName<Button>("BreedButton").IsVisible = false;
                }
            }
          
        }

        async void AddPhoto_Clicked(object sender, EventArgs args)
        {
            var todoItem = (Cat)BindingContext;
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
                    await UserDialogs.Instance.AlertAsync(""+this.GetType() , " Empty Image");
                    return;
                }

                var memoryStream = new MemoryStream();

                selectedImageFile.GetStream().CopyTo(memoryStream);
                selectedImageFile.Dispose();

                var converted = memoryStream.ToArray();
                todoItem.icon = DependencyService.Get<IMediaService>().ResizeImage(converted, 128, 128);
                todoItem.image = DependencyService.Get<IMediaService>().ResizeImage(converted, 512, 512);

                selectedImage.Source = todoItem.ImageAsImageStream;

            }
            catch (Exception e)
            {
                var text = "";
                await UserDialogs.Instance.AlertAsync("" + this.GetType(), " Error uploading message");

                return;
            }
        }

        async void ButtonClicked(object sender, EventArgs args)
	    {
	        var newCat = (Cat)BindingContext;
	        if (newCat.Gender == null)
	        {
	            DisplayAlert("Ошибка", "Задайте пол животного", "ОК");
	        }
	        else
	        {
	            await App.Database2.SaveItemAsync(newCat);
	            await Navigation.PopAsync();
	        }
	    }

        async void DeleteClicked(object sender, EventArgs args)
        {
            var todoItem = (Cat)BindingContext;
            await App.Database2.DeleteItemAsync(todoItem);
            await Navigation.PopAsync();
        }

        async void BreedClicked(object sender, EventArgs args)
	    {
	        Cat todoCat = (Cat)BindingContext;
            Vyazka vyazka =new Vyazka();
	        if (todoCat.Gender == null)
	        {
	            DisplayAlert("Ошибка", "Задайте пол животного", "ОК");
	        }
            else if (todoCat.Gender.Equals("Мальчик"))
	        {
	            vyazka.Father = todoCat.Text;
	            vyazka.FatherId = todoCat.Id;
	            Navigation.RemovePage(this);
                await Navigation.PushAsync(new FemalesViewPage(vyazka));
            }
            else
	        {
	            vyazka.Mother = todoCat.Text;
	            vyazka.MotherId = todoCat.Id;
                Navigation.RemovePage(this);
	            await Navigation.PushAsync(new MalesViewPage(vyazka));
            }
        }

        async void PregnantClicked(object sender, EventArgs args)
        {
            Cat todoCat = (Cat)BindingContext;
            Pregnancy pregnancy = new Pregnancy();
            if (todoCat.Gender == null)
            {
                DisplayAlert("Ошибка", "Задайте пол животного", "ОК");
            }
            else if (todoCat.Gender.Equals("Мальчик"))
            {
                DisplayAlert("Ошибка", "Мальчики не беременеют", "ОК");
            }
            else
            {
                pregnancy.Mother = todoCat.Text;
                pregnancy.MotherId = todoCat.Id;
                Navigation.RemovePage(this);
                await Navigation.PushAsync(new MalesViewPage(pregnancy));
            }


       
        }

        async void GaveBirthClicked(object sender, EventArgs args)
        {
            Cat todoCat = (Cat) BindingContext;
            Pomet pomet = new Pomet();
            if (todoCat.Gender == null)
            {
                DisplayAlert("Ошибка", "Задайте пол животного", "ОК");
            }
            else if (todoCat.Gender.Equals("Мальчик"))
            {
                DisplayAlert("Ошибка", "Мальчики не рожают", "ОК");
            }
            else
            {
                pomet.Mother = todoCat.Text;
                pomet.MotherId = todoCat.Id;
                pomet.PersonId = App.PersonId;
                Navigation.RemovePage(this);
                await Navigation.PushAsync(new MalesViewPage(pomet));
            }
        }

    }
}