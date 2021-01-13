using System;
using System.Collections.Generic;
using System.IO;
using Acr.UserDialogs;
using FFImageLoading.Forms;
using MaxWell.Controls.Recipes;
using MaxWell.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MaxWell.Models;
using MaxWell.Shared.Models;
using MaxWell.Services;
using MaxWell.ViewModels.Recipes;
using ImageCircle.Forms.Plugin.Abstractions;
using MaxWell.Shared.Models.Foods.Plans;
using Plugin.Media;
using Plugin.Media.Abstractions;


namespace MaxWell.Views.Recipes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeDetailViewPage : ContentPage
    {
        bool isNewItem;

        private ContentPage _recipeDetailViewPage;

        public RecipeDetailViewPage(bool isNew = false)
        {
            InitializeComponent();
            isNewItem = isNew;
            if(isNewItem) Title = "Create".Translate();
            else Title = "Edit".Translate();
        }

        public RecipeDetailViewPage()
        {
            InitializeComponent();
           Title = "Create".Translate();
            
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
           await recipeDetailView.PopulateLists();
            if (!isNewItem)
                if (!vm.Recipe.PersonId.Equals(await App.GetInstance().GetVkUserId()))
            {
                ToolbarItems.Clear();
                recipeDetailView.HideButton();
            }
        }

        public RecipeDetailViewPage(Recipe recipe, bool isNew = false)
        {
            InitializeComponent();
            vm = new RecipeDetailViewModel(recipe);
            this.BindingContext = vm;
            isNewItem = isNew;

          

        }

        RecipeDetailViewModel vm;
        async void RecipePhotoClicked(object sender, EventArgs args)
        {

        try
        {
                /* 
                    byte[] image= await CameraService.GetInstance().GetImageBytes();
                    RemoteImage RemoteImage = new RemoteImage();
                RemoteImage.DownloadedImageBlob = image;
               RemoteImage =  await App.RemoteImageManager.SaveTaskAsync(RemoteImage, true);
               string //RemoteImage.ImageUrl;
              */
            string url=await CameraService.GetInstance().UploadPhoto(); 
            vm.Recipe.ImageUrl = url;

            recipeDetailView.FindByName<CachedImage>("selectedImage").Source = url;



        }
            catch (Exception e)
            {
                await DisplayAlert("Error uploading image", "" + e.Message + "|" + e.StackTrace, "OK");
                return;
            }
        }
        
        async void RecipeSaveClicked(object sender, EventArgs args)
        {
            var recipe = ((RecipeDetailViewModel)BindingContext).Recipe;
            if (recipe.RecipeId != 0) isNewItem = false;
            if (recipe.Name == null)
            {
                DisplayAlert("Ошибка", "Задайте Название", "ОК");
            }
            else
            {
                try
                {
                    await App.RecipeManager.SaveRecipeAsync(recipe, isNewItem);
                }
                catch (Exception e)
                {
                    UserDialogs.Instance.AlertAsync(e.Message);
                }
                await Navigation.PopAsync();
            }
        }

        async void RecipeDeleteClicked(object sender, EventArgs args)
        {
            var recipe = ((RecipeDetailViewModel)BindingContext).Recipe;

            await App.RecipeManager.DeleteRecipeAsync(recipe);
            await Navigation.PopAsync();
        }
    }
}