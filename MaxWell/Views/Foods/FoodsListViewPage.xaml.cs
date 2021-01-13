using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MaxWell.Helpers;
using MaxWell.Models;
using MaxWell.Resources;
using MaxWell.Services;
using MaxWell.Shared.Models.Foods.Plans;
using MaxWell.ViewModels;
using MaxWell.ViewModels.Comments;
using MaxWell.ViewModels.Foods;
using MaxWell.ViewModels.Pomets;
using MaxWell.Views;

namespace MaxWell.Views.Foods
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FoodsListViewPage : ContentPage
	{
	    FoodListViewModel viewModel;
	    private Ingredient Ingredient;
        public FoodsListViewPage()
        {
            InitializeComponent();
        //    Title = AppResources.FoodPageTitle;
            Title = "Food".Translate();
            BindingContext = viewModel = new FoodListViewModel();
          
         }
	    public FoodsListViewPage(Ingredient ingredient)
	    {
	        InitializeComponent();
	     //   Title = AppResources.FoodPageTitle;
	        Title = "Food".Translate();
            BindingContext = viewModel = new FoodListViewModel();
	        Ingredient = ingredient;
	    }
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem != null)
            {
                FoodListItemViewModel model = (FoodListItemViewModel)args.SelectedItem;
                if (Ingredient == null)
                    await Navigation.PushAsync(new FoodDetailViewPage(model.Food));
                else
                {
                    try
                    {
                        Ingredient.Name = model.Food.Name;
                        Ingredient.PersonId = await App.GetInstance().GetVkUserId();
                        Ingredient.FoodId = model.Food.FoodId;
                        Ingredient.ImageUrl = model.Food.Description;
                        Ingredient.FoodDescriptionId = (int) model.Food.FoodDescriptionId;

                        PromptConfig config = new PromptConfig();
                        config.InputType = InputType.DecimalNumber;
                        config.Placeholder = "100";
                        config.Title = "Вес (граммов)";

                        PromptResult pResult = await UserDialogs.Instance.PromptAsync(config);
                        if (pResult.Ok && !string.IsNullOrWhiteSpace(pResult.Text))
                        {
                            Ingredient.Amount = Convert.ToDouble(pResult.Text);
                           await App.IngredientManager.SaveIngredientAsync(Ingredient,false);
                        }
                    }
                    catch (Exception e)
                    {
                        UserDialogs.Instance.AlertAsync(e.Message);

                    }

                    await Navigation.PopModalAsync();
                }

            }
        }
	
        async void RestAddItem_Clicked(object sender, EventArgs e)
        {
            //ZZ
            if ((await App.GetInstance().GetLoggedInPerson()) != null)
            await Navigation.PushAsync(new FoodDetailViewPage(new Food()
            {
                VKUserId = (await App.GetInstance().GetLoggedInPerson()).Id.ToString(),
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
            var loading = UserDialogs.Instance.Loading(AppResources.Loading, null, null, true);
            await viewModel.LoadData();
            loading.Hide();
            
        }


	    public async void Speak(object sender, EventArgs eventArgs)
	    {
	        foreach (var foodVm in viewModel.FoodModelList)
	        {

	            await VoiceService.getInstance().SpeakRu(foodVm.Food.Name);
	            await VoiceService.getInstance().SpeakEn(foodVm.Food.NameEn);
	            await VoiceService.getInstance().SpeakEn("100 граммов содержит"); 
	            await VoiceService.getInstance().SpeakRu(foodVm.Food.Protein_g + " граммов белков");
	            await VoiceService.getInstance().SpeakRu(foodVm.Food.Fats_g + " граммов жиров");
	            await VoiceService.getInstance().SpeakRu(foodVm.Food.Carbs_g + " граммов углеводов");
	            await VoiceService.getInstance().SpeakRu(foodVm.Food.Calcium_mg + " граммов Кальция");
                await VoiceService.getInstance().SpeakRu(foodVm.Food.Phenylalanine_g + " граммов фенил ала-нина");
	         


	        }
        }
	   
    }
}