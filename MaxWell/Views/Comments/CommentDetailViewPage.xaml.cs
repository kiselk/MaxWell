using System;
using System.IO;
using MaxWell.Controls.Comments;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MaxWell.Models;
using MaxWell.Services;
using MaxWell.ViewModels.Cats;
using MaxWell.ViewModels.Comments;
using ImageCircle.Forms.Plugin.Abstractions;
using Plugin.Media;
using Plugin.Media.Abstractions;


namespace MaxWell.Views.Comments
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CommentDetailViewPage : ContentPage
    {

        public CommentDetailViewPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            vm = (CommentDetailViewModel)BindingContext;
        }
        public CommentDetailViewPage(Comment comment)
        {
            InitializeComponent();
            vm = new CommentDetailViewModel(comment);
            this.BindingContext = vm;
        }
        CommentDetailViewModel vm;
        async void AddPhoto_Clicked(object sender, EventArgs args)
        {
            var todoItem = ((CommentDetailViewModel)BindingContext).Comment;
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
                commentEditView.FindByName<CircleImage>("selectedImage").Source = todoItem.ImageAsImageStream;
              //  commentEditView.selectedImage.Source = todoItem.ImageAsImageStream;

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
	        var newCat = ((CommentDetailViewModel)BindingContext).Comment;
	        if (newCat.Text == null)
	        {
	           await DisplayAlert("Ошибка", "Задайте текст комментария", "ОК");
	        }
	        else
	        {
	            await App.Database2.SaveItemAsync(newCat);
	            await Navigation.PopAsync();
	        }
	    }

        async void DeleteClicked(object sender, EventArgs args)
        {
            var todoItem = ((CommentDetailViewModel)BindingContext).Comment;
            await App.Database2.DeleteItemAsync(todoItem);
            await Navigation.PopAsync();
        }

      
   

    }
}