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
using MaxWell.ViewModels.Ingredients;
using MaxWell.ViewModels.Pomets;
using MaxWell.Views;

namespace MaxWell.Views.Ingredients
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IngredientListViewPage : ContentPage
    {
        IngredientListViewModel viewModel;
   
        public IngredientListViewPage()
        {
            InitializeComponent();
                //Title = CustomConstants.Ingredients.IngredientConstants.IngredientsName;
            Title = "Ingredients".Translate();
            BindingContext = viewModel = new IngredientListViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem != null)
            {
                IngredientListItemViewModel model = (IngredientListItemViewModel)args.SelectedItem;
                await Navigation.PushAsync(new IngredientDetailViewPage(model.Ingredient));
            }
        }
	
        async void IngredientAddClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new IngredientDetailViewPage(new Ingredient()
            {
                PersonId = await App.Instance.GetVkUserId(),
                CreateDateTime = DateTime.Now
            } ,true));
        }
	  
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var loading = UserDialogs.Instance.Loading("Ingredient", null, null, true);
            await viewModel.LoadData();
            loading.Hide();
        }
    }
}