using MaxWell.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows.Input;
using MaxWell.ViewModels.Pomets;
using Xamarin.Forms;
using Xamvvm;

namespace MaxWell.ViewModels.Prides
{
    class PrideListViewModel : BasePageModel

    {
        public ObservableCollection<PrideListItemViewModel> PrideModelList
        {
            get { return GetField<ObservableCollection<PrideListItemViewModel>>(); }
            set { SetField(value); }
        }

        public PrideListViewModel()
        {
            PrideModelList = new ObservableCollection<PrideListItemViewModel>();


        }
    }
}
