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
using MaxWell.ViewModels.Meals;
using MaxWell.ViewModels.Prides;
using MaxWell.Views.Cats;
using MaxWell.Views.Meals;
using MaxWell.Views.Main.Deprecated.UI;
using MaxWell.Views.Prides;
using Syncfusion.SfCarousel.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace MaxWell.Controls.Meals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MealDetailView : ContentView
    {
        private MealDetailViewModel vm;

        public MealDetailView()
        {
            InitializeComponent();
        }

        public async void LoadImage(object sendera, EventArgs e)
        {
            LoadImage();
        }

        public async void LoadImage()
        {
            if (vm.Meal.Name!=null) 
            if (!vm.Meal.Name.Equals("")) 
            {
            var loading = UserDialogs.Instance.Loading("Изображение", null, null, true);

            try
            {
                await Task.Yield();
                var imageUrl = GoogleService.getInstance().getImage(vm.Meal.Name).Result;
                var downloadedImage = ImageHelper.ImageUrlToByteArray(imageUrl);
                vm.Meal.ImageUrl = imageUrl;
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