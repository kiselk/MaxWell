using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MaxWell.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MaxWell.Models;
using MaxWell.Shared.Models;
using MaxWell.Shared.Models.Foods.Plans;
using MaxWell.ViewModels;
using MaxWell.ViewModels.Comments;
using MaxWell.ViewModels.Meals;
using MaxWell.ViewModels.Pomets;
using MaxWell.Views;

namespace MaxWell.Views.Meals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MealListViewPage : ContentPage
    {
        MealListViewModel viewModel;
   
        public MealListViewPage()
        {
            InitializeComponent();
            Title = CustomConstants.Meals.MealConstants.MealsNameEn.Translate();
            BindingContext = viewModel = new MealListViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem != null)
            {
                MealListItemViewModel model = (MealListItemViewModel)args.SelectedItem;
                await Navigation.PushAsync(new MealDetailViewPage(model.Meal));
            }
        }
	
        async void MealAddClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MealDetailViewPage(new Meal()
            {
                CreateDateTime = DateTime.Now
            } ,true));
        }
	  
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var loading = UserDialogs.Instance.Loading("Loading".Translate(), null, null, true);
            await viewModel.LoadData();
            loading.Hide();
        }
    }
}