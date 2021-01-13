using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MaxWell.Models;
using MaxWell.Models.Foods;
using MaxWell.ViewModels;
using MaxWell.ViewModels.Comments;
using MaxWell.ViewModels.Foods;
using MaxWell.ViewModels.Images;
using MaxWell.ViewModels.Pomets;
using MaxWell.Views;

namespace MaxWell.Views.Foods
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ImagesListViewPage : ContentPage
	{
	    ImageListViewModel viewModel;
        public ImagesListViewPage(Food food,FoodDescription foodDescription)
        {
            InitializeComponent();
            Title = foodDescription.Name;
             BindingContext = viewModel = new ImageListViewModel();
             viewModel.FoodDescription = foodDescription;
            viewModel.Food = food;
           // viewModel = new FoodListViewModel();
           // viewModel =(FoodListViewModel) BindingContext;
          //  Source = new ObservableCollection<FoodListItemViewModel>();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem != null)
            {
                ImageListItemViewModel model = (ImageListItemViewModel)args.SelectedItem;
                model.Food.Description = model.ImageUrl;
                Navigation.PopAsync();// await Navigation.PushAsync(new FoodDetailViewPage(model.Food));

            }
        }
	
        async void RestAddItem_Clicked(object sender, EventArgs e)
        {
            //ZZ
            await Navigation.PushAsync(new FoodDetailViewPage(new Food()
            {
                Calcium_mg = 0.0,
                Carbs_g =0,
                Fats_g = 0,
                Protein_g = 0,
                Phenylalanine_g  =0
            } ,true));
        }
	  
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var loading = UserDialogs.Instance.Loading("Фото", null, null, true);
            await viewModel.LoadData();
            loading.Hide();
        }



	   
    }
}