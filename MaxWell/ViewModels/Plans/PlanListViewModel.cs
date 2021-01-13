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

namespace MaxWell.ViewModels.Plans
{
    class PlanListViewModel : BasePageModel
    {

        public ObservableCollection<PlanListItemViewModel> PlanModelList
        {
            get { return GetField<ObservableCollection<PlanListItemViewModel>>(); }
            set { SetField(value); }
        }
        public Boolean IsBusy
        {
            get { return GetField<Boolean>(); }
            set { SetField(value); }
        }
        public PlanListViewModel()
        {
            PlanModelList = new ObservableCollection<PlanListItemViewModel>();
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
                PlanModelList.Clear();

                var plans = await App.PlanManager.GetPlansAsync();

                foreach (var plan in plans)
                {
                    PlanModelList.Add(new PlanListItemViewModel(plan));
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
