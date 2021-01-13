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
using MaxWell.Models.Foods;
using MaxWell.Services;
using MaxWell.ViewModels.Comments;
using MaxWell.ViewModels.Foods;
using MaxWell.ViewModels.Prides;
using MaxWell.Views.Cats;
using MaxWell.Views.Foods;
using MaxWell.Views.Main.Deprecated.UI;
using MaxWell.Views.Prides;
using Plugin.TextToSpeech;
using Syncfusion.SfCarousel.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace MaxWell.Controls.Foods
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodDetailView : ContentView
    {
        private FoodDetailViewModel vm;


        private List<FoodDescription> foodDescriptions;

        public FoodDetailView()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<byte[]>(this, "ImageSelected", (args) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    //Set the source of the image view with the byte array
                    //   vm.Food.image = MediaService.getInstance().ResizeImage((byte[]) args, 600, 200);
                    //   selectedImage.Source = vm.Food.ImageAsImageStream;
                    // selectedImage.Source = ImageSource.FromStream(() => new MemoryStream(MediaService.getInstance().ResizeImage(downloadedImage, 600, 200)));
                    //   selectedImage.Source = vm.Food.Description;
                    //   vm.Food.Description = "empty";

                    selectedImage.HeightRequest = 200;
                });
            });

        }


        public async void LoadImage(object sendera, EventArgs e)
        {
            LoadImage();
        }

        public async void LoadImage()
        {
            var loading = UserDialogs.Instance.Loading("Изображение", null, null, true);

            try
            {
                vm = (FoodDetailViewModel) BindingContext;
                if (vm.Food.Description == null)
                {
                    await Task.Yield();
                    // UserDialogs.Instance.AlertAsync("XPOS: " + selectedImage.X);
                    //    await pageScrollView.ScrollToAsync(selectedImage.X, selectedImage.Y, false);
                    var imageUrl = GoogleService.getInstance().getImage(vm.Food.NameEn).Result;
                    var downloadedImage = ImageHelper.ImageUrlToByteArray(imageUrl);
                    vm.Food.Description = imageUrl;
                    //    vm.Food.image = MediaService.getInstance().ResizeImage(downloadedImage, 600, 200);
                    //vm.Food.Description = imageUrl;

                    selectedImage.Source = imageUrl;
                  //ZZZ  selectedImage.Source = ImageSource.FromStream(() => new MemoryStream(MediaService.getInstance().ResizeImage(downloadedImage, 500, 200)));

                    selectedImage.HeightRequest = 200;
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.AlertAsync(ex.Message);
            }

            loading.Hide();
        }

        public async void LoadDescriptions(string text)
        {
            var loading = UserDialogs.Instance.Loading("Описания", null, null, true);
            try
            {
                List<FoodDescription> foodDescList = await App.FoodManager.GetFoodDescriptionsAsync(text);

                InfoList.ItemsSource = foodDescList;

            }
            catch (Exception ex)
            {
                UserDialogs.Instance.AlertAsync(ex.Message);
            }

            loading.Hide();
        }

        private List<NutritionData> nutritionDataList;

        public async void LoadNutrition()
        {
            var loading = UserDialogs.Instance.Loading("Данные", null, null, true);

            try
            {


                nutritionDataList = await App.FoodManager.GetNutritionDataAsync(vm.Food.FoodDescriptionId.ToString());
                NutritionList.ItemsSource = nutritionDataList.OrderBy(item => item.NutritionDefinition.Unknown2);
                NutritionList.HeightRequest = nutritionDataList.Count * 40 + 300;
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.AlertAsync(ex.Message);
            }

            loading.Hide();
        }

        public void DoFocus()
        {
            editorForNameEntry.Focus();
        }

        public async Task PopulateLists()
        {
            vm = (FoodDetailViewModel) BindingContext;
            try
            {
                if (vm.Food.Description != null)
                {
                    selectedImage.HeightRequest = 200;
                }
                else
                {
                }

                if (vm.Food.FoodDescriptionId != null)
                {
                    InfoList.IsVisible = false;
                    ContainsLabelRu.IsVisible = true;
                    //   ContainsLabelEn.IsVisible = true;
                    NutritionList.IsVisible = true;
                    LoadNutrition();
                    if (vm.Food.image == null)
                    {
                        LoadImage();
                    }

                    //     InfoCount.Text = "Nutrition Info Count: " + vm.Food.NutritionDataCollection.Count;
                }
            }
            catch (Exception e)
            {
                UserDialogs.Instance.AlertAsync(e.Message);
            }

        }

        public async void OnTextChanged(object sendera, EventArgs e)
        {

            Entry editor = (sendera as Entry);
            if (editor.Text != "")
            {
                //  await Task.Yield();
                //  await pageScrollView.ScrollToAsync(InfoList.X, InfoList.Y-40, false);
                //    LoadDescriptions(editor.Text);
            }

        }

        public void OnItemSelected(object sendera, SelectedItemChangedEventArgs e)
        {
            try
            {
                vm = (FoodDetailViewModel) BindingContext;
                InfoList.IsVisible = false;

                ContainsLabelRu.IsVisible = true;
                //   ContainsLabelEn.IsVisible = true;
                NutritionList.IsVisible = true;
                FoodDescription selectedFoodDescription = (e.SelectedItem as FoodDescription);
                //   Navigation.PushAsync(new ImagesListViewPage(vm.Food, selectedFoodDescription));
                editorForNameEntry.Text = selectedFoodDescription.Name;
                FoodDescriptionLabel.Text = selectedFoodDescription.Name;
                FoodDescriptionLabelEn.Text = selectedFoodDescription.NameEn;
                vm.Food.Name = selectedFoodDescription.Name;
                vm.Food.NameEn = selectedFoodDescription.NameEn;
                vm.Food.FoodDescriptionId = selectedFoodDescription.FoodDescriptionId;
                LoadImage();
                LoadNutrition();

            }
            catch (Exception ex)
            {
                UserDialogs.Instance.AlertAsync(ex.Message);
            }
        }

        public async void LostFocus(object sender, EventArgs e)
        {

        }

        public async void OnFocus(object sender, EventArgs e)
        {
            try
            {
                //   await pageScrollView.ScrollToAsync(editorForName, ScrollToPosition.Start, true);


            }
            catch (Exception ex)
            {
                UserDialogs.Instance.AlertAsync(ex.Message);
            }
        }

        public async void OnSearchClicked(object obj, EventArgs e)
        {
            //  LoadImage();
            LoadDescriptions(editorForNameEntry.Text);
            InfoList.IsVisible = true;
            NutritionList.IsVisible = false;
            ContainsLabelRu.IsVisible = false;
            //  ContainsLabelEn.IsVisible = false;
        }

        private ViewCell oldCell = null;

        private async void OnNutrientSelected(object sender, EventArgs e)
        {
            try
            {


                var viewCell = (ViewCell) sender;
                if (viewCell.View != null)
                {
                    if (oldCell != null) oldCell.View.BackgroundColor = Color.Black;
                    viewCell.View.BackgroundColor = Color.DimGray;
                    oldCell = viewCell;
                }

            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Alert(ex.Message);
            }
        }

        public async void TakePhoto(object sender, EventArgs args)
        {


            Device.BeginInvokeOnMainThread(async () =>
            {
                //Ask the user if they want to use the camera or pick from the gallery
                var action = await UserDialogs.Instance.ActionSheetAsync("Add Photo", "Cancel", null, null, "Choose Existing", "Take Photo");
                //   var action = await UserDialogs.Instance.Aler ("Add Photo", "Cancel", null, "Choose Existing", "Take Photo");

                if (action == "Choose Existing")
                {
                    DependencyService.Get<ICameraInterface>().BringUpPhotoGallery();
                }
                else if (action == "Take Photo")
                {
                    DependencyService.Get<ICameraInterface>().BringUpCamera();
                }
            });
        }

        public async Task Speak()
        {
            VoiceService.getInstance().SpeakRu(ContainsLabelRu.Text);
            //    VoiceService.SpeakEn(ContainsLabelEn.Text);

            foreach (var nutr in nutritionDataList)
            {

               await VoiceService.getInstance().SpeakRu(nutr.Amount1 + " " + nutr.NutritionDefinition.Measurement + " " + nutr.NutritionDefinition.Name);
               // await VoiceService.getInstance().SpeakEn(nutr.NutritionDefinition.NameEn);
            }

         
        }
    }
}