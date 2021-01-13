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
using MaxWell.ViewModels.Recipes;
using MaxWell.ViewModels.Pomets;
using MaxWell.Views;

namespace MaxWell.Views.Recipes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeListViewPage : ContentPage
    {
        RecipeListViewModel viewModel;
   
        public RecipeListViewPage()
        {
            InitializeComponent();
            Title = CustomConstants.Recipes.RecipeConstants.RecipesNameEn.Translate();
            BindingContext = viewModel = new RecipeListViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem != null)
            {
                RecipeListItemViewModel model = (RecipeListItemViewModel)args.SelectedItem;
                await Navigation.PushAsync(new RecipeDetailViewPage(model.Recipe));
            }
        }
	
        async void RecipeAddClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecipeDetailViewPage(new Recipe()
            {
                PersonId= await App.Instance.GetVkUserId(),
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