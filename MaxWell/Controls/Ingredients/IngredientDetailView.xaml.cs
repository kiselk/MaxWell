using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MaxWell.Helpers;
using MaxWell.Models;
using MaxWell.Shared.Models;
using MaxWell.Services;
using MaxWell.ViewModels.Comments;
using MaxWell.ViewModels.Ingredients;
using MaxWell.ViewModels.Prides;
using MaxWell.Views.Cats;
using MaxWell.Views.Ingredients;
using MaxWell.Views.Main.Deprecated.UI;
using MaxWell.Views.Prides;
using Syncfusion.SfCarousel.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace MaxWell.Controls.Ingredients
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IngredientDetailView : ContentView
    {
        private IngredientDetailViewModel vm;

        public IngredientDetailView()
        {
            InitializeComponent();
        }

        public async void LoadImage(object sendera, EventArgs e)
        {
            LoadImage();
        }

        public async void LoadImage()
        {
            if (vm.Ingredient.Name!=null) 
            if (!vm.Ingredient.Name.Equals("")) 
            {
            var loading = UserDialogs.Instance.Loading("Изображение", null, null, true);

            try
            {
                await Task.Yield();
                var imageUrl = GoogleService.getInstance().getImage(vm.Ingredient.Name).Result;
                var downloadedImage = ImageHelper.ImageUrlToByteArray(imageUrl);
                vm.Ingredient.ImageUrl = imageUrl;
                selectedImage.Source = ImageSource.FromStream(() => new MemoryStream(MediaService.getInstance().ResizeImage(downloadedImage, 500, 200)));
                selectedImage.HeightRequest = 200;
           }
            catch (Exception ex)
            {
                UserDialogs.Instance.AlertAsync(ex.Message);
            }

            loading.Hide();
            }
        }


    }
}