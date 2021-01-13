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

namespace MaxWell.ViewModels.Foods
{
    class FoodListViewModel : BasePageModel
    {

        public ObservableCollection<FoodListItemViewModel> FoodModelList
        {
            get { return GetField<ObservableCollection<FoodListItemViewModel>>(); }
            set { SetField(value); }
        }
        public Boolean IsBusy
        {
            get { return GetField<Boolean>(); }
            set { SetField(value); }
        }
        public FoodListViewModel()
        {
            FoodModelList = new ObservableCollection<FoodListItemViewModel>();
            _source = new ObservableCollection<FoodListItemViewModel>();


        }

        private ObservableCollection<FoodListItemViewModel> _source;
        public ObservableCollection<FoodListItemViewModel> Source
        {
            get
            {
                return _source;
            }
            set
            {
                _source = value;
                OnPropertyChanged(nameof(Source));
            }
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
           var loading = UserDialogs.Instance.Loading("Изображение", null, null, true);
            try
            {


                var foods = await App.FoodManager.GetFoodsForCurrentUser();
                    FoodModelList.Clear();

                foreach (var food in foods)
                    {
                        FoodModelList.Add(new FoodListItemViewModel(food));
                    }
               
            }
            catch (Exception e)
            {
                UserDialogs.Instance.AlertAsync(e.Message, "" + this.GetType() + " List Error");
            }


            loading.Hide();

        }
        
    }

}
