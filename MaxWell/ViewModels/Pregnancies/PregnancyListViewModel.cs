using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using MaxWell.ViewModels.Vyazki;
using Xamvvm;

namespace MaxWell.ViewModels.Pregnancies
{
    class PregnancyListViewModel : BasePageModel
    {

        public ObservableCollection<PregnancyListItemViewModel> PregnancyModelList
        {
            get { return GetField<ObservableCollection<PregnancyListItemViewModel>>(); }
            set { SetField(value); }
        }

        public PregnancyListViewModel()
        {
            PregnancyModelList = new ObservableCollection<PregnancyListItemViewModel>();


        }
    }
}
