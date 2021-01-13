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

namespace MaxWell.ViewModels.Persons
{
    class RestPersonListViewModel : BasePageModel
    {

        public ObservableCollection<PersonListItemViewModel> PersonModelList
        {
            get { return GetField<ObservableCollection<PersonListItemViewModel>>(); }
            set { SetField(value); }
        }
        public Boolean IsBusy
        {
            get { return GetField<Boolean>(); }
            set { SetField(value); }
        }
        public RestPersonListViewModel()
        {
            PersonModelList = new ObservableCollection<PersonListItemViewModel>();
            _source = new ObservableCollection<PersonListItemViewModel>();


        }

        private ObservableCollection<PersonListItemViewModel> _source;
        public ObservableCollection<PersonListItemViewModel> Source
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
            try
            {
                var persons = await App.PersonManager.GetTasksAsync();
             //   UserDialogs.Instance.AlertAsync(""+persons.Count, "" + this.GetType() + " persons");
              //  viewModel = (RestPersonListViewModel)BindingContext;
                PersonModelList.Clear();
                
                foreach (var person in persons)
                {
                    //      UserDialogs.Instance.AlertAsync(person.Name, "" + this.GetType() + " person");
                    PersonListItemViewModel model = new PersonListItemViewModel(person);

                    if (!PersonModelList.Contains(model))
                    {   await model.LoadFoods();
                        PersonModelList.Add(model);
                     
                    }
                }
               
            }
            catch (Exception e)
            {
                UserDialogs.Instance.AlertAsync(e.Message,""+this.GetType() + " List Error");
            }

        }
        
    }

}
