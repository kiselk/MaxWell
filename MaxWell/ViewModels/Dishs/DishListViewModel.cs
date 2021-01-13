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

namespace MaxWell.ViewModels.Dishs
{
    class DishListViewModel : BasePageModel
    {

        public ObservableCollection<DishListItemViewModel> DishModelList
        {
            get { return GetField<ObservableCollection<DishListItemViewModel>>(); }
            set { SetField(value); }
        }
        public Boolean IsBusy
        {
            get { return GetField<Boolean>(); }
            set { SetField(value); }
        }
        public DishListViewModel()
        {
            DishModelList = new ObservableCollection<DishListItemViewModel>();
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
                DishModelList.Clear();

                var dishs = await App.DishManager.GetDishsAsync();

                foreach (var dish in dishs)
                {
                    DishModelList.Add(new DishListItemViewModel(dish));
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
