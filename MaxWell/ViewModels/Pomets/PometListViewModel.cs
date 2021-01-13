using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using MaxWell.ViewModels.Pregnancies;
using Xamvvm;

namespace MaxWell.ViewModels.Pomets
{
    class PometListViewModel : BasePageModel
    {

        public ObservableCollection<PometListItemViewModel> PometModelList
        {
            get { return GetField<ObservableCollection<PometListItemViewModel>>(); }
            set { SetField(value); }
        }

        public PometListViewModel()
        {
            PometModelList = new ObservableCollection<PometListItemViewModel>();


        }
    }
}
