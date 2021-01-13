using System;
using System.IO;
using Acr.UserDialogs;
using MaxWell.Models;
using MaxWell.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace MaxWell.Views.Todo
{
	public partial class TodoItemPage : ContentPage
	{
		bool isNewItem;

		public TodoItemPage (bool isNew = false)
		{
			InitializeComponent ();
			isNewItem = isNew;
		}

		async void OnSaveActivated (object sender, EventArgs e)
		{
			var todoItem = (TodoItem)BindingContext;
			await App.TodoManager.SaveTaskAsync (todoItem, isNewItem);
			await Navigation.PopAsync ();
		}

		async void OnDeleteActivated (object sender, EventArgs e)
		{
			var todoItem = (TodoItem)BindingContext;
			await App.TodoManager.DeleteTaskAsync (todoItem);
			await Navigation.PopAsync ();
		}

		void OnCancelActivated (object sender, EventArgs e)
		{
			Navigation.PopAsync ();
		}

		void OnSpeakActivated (object sender, EventArgs e)
		{
			var todoItem = (TodoItem)BindingContext;
			App.Speech.Speak (todoItem.Name + " " + todoItem.Notes);
		}

        async void AddPhoto_Clicked(object sender, EventArgs args)
        {
            //ZZ
            var todoItem = ((TodoItem)BindingContext);
            // DisplayAlert("Сохранено", "" , "OK");
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Не поддерживается", "Устройство не поддерживает эту функциональность", "1");
                return;
            }
            else if (!(CrossMedia.Current.IsTakePhotoSupported && CrossMedia.Current.IsCameraAvailable))
            {
                await DisplayAlert("Не поддерживается", "Устройство не поддерживает эту функциональность", "1");
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
                    MaxWidthHeight = 1000,
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
                todoItem.image = DependencyService.Get<IMediaService>().ResizeImage(converted, 512, 512);

                this.FindByName<Image>("selectedImage").Source = todoItem.ImageAsImageStream;

            }
            catch (Exception e)
            {
                var text = "";

                await DisplayAlert("Error uploading message", "" + e.Message + "|" + e.StackTrace, "OK");
                return;
            }
        }


    }
}
