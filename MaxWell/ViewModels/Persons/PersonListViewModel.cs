using MaxWell.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows.Input;
using MaxWell.ViewModels.Comments;
using MaxWell.ViewModels.Pomets;
using Xamarin.Forms;
using Xamvvm;

namespace MaxWell.ViewModels.Persons
{
    class PersonListViewModel : BasePageModel
    {

        public ObservableCollection<PersonListItemViewModel> PersonModelList
        {
            get { return GetField<ObservableCollection<PersonListItemViewModel>>(); }
            set { SetField(value); }
        }

        public PersonListViewModel()
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
    }

}
