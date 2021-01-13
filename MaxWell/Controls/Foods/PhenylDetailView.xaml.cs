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
using MaxWell.Views.Prides;
using Syncfusion.SfCarousel.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaxWell.Controls.Foods
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhenylDetailView : ContentView
    {
        private FoodDetailViewModel vm;
        

        private List<FoodDescription> foodDescriptions;

        public PhenylDetailView()
        {
            InitializeComponent();


        }

  
        public async void LoadImage(object sendera, EventArgs e)
        {
           // LoadImage();
        }

        public async void LoadImage()
        {  var loading = UserDialogs.Instance.Loading("Изображение",null,null,true);
             
            try
            {
                 await Task.Yield();
               // UserDialogs.Instance.AlertAsync("XPOS: " + selectedImage.X);
            //    await pageScrollView.ScrollToAsync(selectedImage.X, selectedImage.Y, false);
                var imageUrl = GoogleService.getInstance().getImage(vm.Food.Name).Result;
              //  var downloadedImage = ImageHelper.ImageUrlToByteArray(imageUrl);
                vm.Food.Description = imageUrl;
              //   vm.Food.image = MediaService.getInstance().ResizeImage(downloadedImage, 600, 200);


               
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.AlertAsync(ex.Message);
            } loading.Hide();
        }
        public async void LoadDescriptions()
        {
            InfoList.IsVisible = true;
            var loading = UserDialogs.Instance.Loading("Описания", null, null, true);
            try
            {
                List<FoodDescription> foodDescList = new List<FoodDescription>();
                if (this.FindByName<Entry>("editorForNameEntry").Text!=null)
                {
                    if (!this.FindByName<Entry>("editorForNameEntry").Text.Equals(""))
                        foodDescList = await App.FoodManager.GetFoodDescriptionsAsync(this.FindByName<Entry>("editorForNameEntry").Text);
                }
                else foodDescList = await App.FoodManager.GetPhenylDescendingFoodDescAsync();

                InfoList.ItemsSource = foodDescList;
            //    InfoList.HeightRequest = foodDescList.Count * 20 + 200;
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.AlertAsync(ex.Message);
            }
          
            loading.Hide();
        }
        public async void LoadNutrition()
        {
            var loading = UserDialogs.Instance.Loading("Данные", null, null, true);

            try
            {
             
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
               
                if (vm.Food.FoodDescriptionId != null)
                {
                     InfoList.IsVisible = false;
                    LoadNutrition();
                 
                }
                //     InfoCount.Text = "Nutrition Info Count: " + vm.Food.NutritionDataCollection.Count;
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
                     InfoList.IsVisible = false;
                FoodDescription selectedFoodDescription = (e.SelectedItem as FoodDescription);
                // editorForNameEntry.Text = selectedFoodDescription.Name;
                FoodDetailViewModel vm = new FoodDetailViewModel();
                vm.Food = new Food();
                vm.Food.Name = selectedFoodDescription.Name;
                vm.Food.NameEn = selectedFoodDescription.NameEn;
                vm.Food.FoodDescriptionId = selectedFoodDescription.FoodDescriptionId;

                Navigation.PushAsync(new FoodDetailViewPage(vm.Food, true));
                //LoadImage();
              //  LoadNutrition();
          
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
           LoadDescriptions();
            InfoList.IsVisible = true;
          }

        private ViewCell oldCell = null;
    
       
        
    }
}