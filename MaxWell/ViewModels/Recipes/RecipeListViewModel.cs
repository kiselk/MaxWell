using MaxWell.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using MaxWell.Helpers;
using MaxWell.ViewModels.Comments;
using MaxWell.ViewModels.Pomets;
using Xamarin.Forms;
using Xamvvm;

namespace MaxWell.ViewModels.Recipes
{
    class RecipeListViewModel : BasePageModel
    {

        public ObservableCollection<RecipeListItemViewModel> RecipeModelList
        {
            get { return GetField<ObservableCollection<RecipeListItemViewModel>>(); }
            set { SetField(value); }
        }
        public Boolean IsBusy
        {
            get { return GetField<Boolean>(); }
            set { SetField(value); }
        }
        public RecipeListViewModel()
        {
            RecipeModelList = new ObservableCollection<RecipeListItemViewModel>();
        }

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsRefreshing = true;

                    await LoadData();

                    IsRefreshing = false;
                });
            }
        }
        public async Task LoadData()
        {
            var loading = UserDialogs.Instance.Loading("Загрузка", null, null, true);
            try
            {
                RecipeModelList.Clear();

                var recipes = await App.RecipeManager.GetRecipesAsync();

                foreach (var recipe in recipes)
                {
                    RecipeModelList.Add(new RecipeListItemViewModel(recipe));
                }
               
            }
            catch (Exception e)
            {
                UserDialogs.Instance.AlertAsync(e.Message,"" + this.GetType() + " LoadData List Error");
            }
            loading.Hide();

        }
        
    }

}
