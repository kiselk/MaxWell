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
using MaxWell.Shared.Models;
using MaxWell.Services;
using MaxWell.Shared.Models.Foods.Plans;
using MaxWell.ViewModels.Comments;
using MaxWell.ViewModels.Ingredients;
using MaxWell.ViewModels.Persons;
using MaxWell.ViewModels.Recipes;
using MaxWell.ViewModels.Prides;
using MaxWell.Views.Cats;
using MaxWell.Views.Foods;
using MaxWell.Views.Ingredients;
using MaxWell.Views.Recipes;
using MaxWell.Views.Main.Deprecated.UI;
using MaxWell.Views.Prides;
using Syncfusion.SfCarousel.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace MaxWell.Controls.Recipes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeDetailView : ContentView
    {
        private RecipeDetailViewModel vm;
        private List<Ingredient> ingredients;

        public RecipeDetailView()
        {
            InitializeComponent();
            vm = (RecipeDetailViewModel)BindingContext;
        }

        public async void LoadImage(object sendera, EventArgs e)
        {
            LoadImage();
        }

        public void HideButton()
        {
            AddIngredientButton.IsVisible = false;
        }

        public async void LoadImage()
        {
            if (vm.Recipe.Name!=null) 
            if (!vm.Recipe.Name.Equals("")) 
            {
            var loading = UserDialogs.Instance.Loading("Изображение", null, null, true);

            try
            {
                await Task.Yield();
                var imageUrl = GoogleService.getInstance().getImage(vm.Recipe.Name).Result;
                var downloadedImage = ImageHelper.ImageUrlToByteArray(imageUrl);
                vm.Recipe.ImageUrl = imageUrl;
               // selectedImage.Source = ImageSource.FromStream(() => new MemoryStream(MediaService.getInstance().ResizeImage(downloadedImage, 500, 200)));
                selectedImage.HeightRequest = 200;
           }
            catch (Exception ex)
            {
                UserDialogs.Instance.AlertAsync(ex.Message);
            }

            loading.Hide();
            }
        }

        public async Task PopulateLists()
        {
            try
            {
                vm = (RecipeDetailViewModel)BindingContext;


                vm.IngredientModelList.Clear();
                vm.Weight = 0;
                ingredients = await App.IngredientManager.GetIngredientsByRecipeId(vm.Recipe.RecipeId);

                foreach (Ingredient ingredient in ingredients)
                {
                    vm.IngredientModelList.Add(new IngredientListItemViewModel(ingredient));
                    vm.Weight += ingredient.Amount;
                }

                LoadNutrition();
              

            }
            catch (Exception e)
            {
                UserDialogs.Instance.AlertAsync(e.Message);
                if (e.InnerException != null)
                {
                    UserDialogs.Instance.AlertAsync(e.InnerException.Message);
                    if (e.InnerException.InnerException != null) UserDialogs.Instance.AlertAsync(e.InnerException.InnerException.Message);
                }
            }
        }
        private async void AddIngredientButtonClicked(object sender, EventArgs e)
        {

           if (vm.Recipe.RecipeId == 0)
            vm.Recipe = await SaveNew(vm.Recipe);

            Ingredient ing = new Ingredient();
          
            //ing.Recipe = vm.Recipe;
            ing.RecipeId = vm.Recipe.RecipeId;
           
            //await App.IngredientManager.SaveIngredientAsync(ing, true);
            await Navigation.PushModalAsync(new FoodsListViewPage(ing));

        }


        private void OnIngredientItemSelected(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new IngredientDetailViewPage((e.Item as IngredientListItemViewModel).Ingredient));
        }

        async Task<Recipe> SaveNew(Recipe recipe)
        {
            if (recipe.Name == null)
            {
                UserDialogs.Instance.AlertAsync("Ошибка", "Задайте Название", "ОК");
            }
            else
            {
                try
                {
                   return await App.RecipeManager.SaveRecipeAsync(recipe, true);
                }
                catch (Exception e)
                {
                    UserDialogs.Instance.AlertAsync(e.Message);
                }

            }
                return null;
        }

        private void OnNutrientSelected(object sender, EventArgs e)
        {

  }
        private List<FoodDescription> foodDescriptions;

        private List<NutritionData> nutritionDataList;

        public void AddNutritionData(NutritionData nutritionaData)
        {
            bool matchFound = false;
            foreach (NutritionData existingData in nutritionDataList)
            {
                if (existingData.NutritionDefinitionId == nutritionaData.NutritionDefinitionId)
                {
                    matchFound = true;
                    existingData.Amount1 += nutritionaData.Amount1;
                    break;
                }

             
            }
            if (!matchFound)
                {
                    nutritionDataList.Add(nutritionaData);
                }

        }
       
        

        public async void LoadNutrition()
            {
                var loading = UserDialogs.Instance.Loading("Данные", null, null, true);

                try
                {
                   nutritionDataList = new List<NutritionData>();
                foreach (Ingredient ingredient in ingredients)
                    {
                      List<NutritionData> IngredientNutritionDataList;
                        IngredientNutritionDataList = await App.FoodManager.GetNutritionDataAsync(ingredient.FoodDescriptionId.ToString());
                        foreach (NutritionData nutritionData in IngredientNutritionDataList)
                        {
                            var value = nutritionData.Amount1; //for 100 g of ingredient
                            nutritionData.Amount1 = nutritionData.Amount1 * ingredient.Amount/100;
                            AddNutritionData(nutritionData);
                        }
                    }
                

               // nutritionDataList = await App.FoodManager.GetNutritionDataAsync(ingredients.FoodDescriptionId.ToString());
                    NutritionList.ItemsSource = nutritionDataList.OrderBy(item => item.NutritionDefinition.Unknown2);
                    NutritionList.HeightRequest = nutritionDataList.Count * 40 ;
                }
                catch (Exception ex)
                {
                    UserDialogs.Instance.AlertAsync(ex.Message);
                }

                loading.Hide();
            }
      
    }
}