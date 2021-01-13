using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamvvm;
namespace MaxWell.ViewModels.Vyazki
{
    class VyazkaListViewModel : BasePageModel
    {

        public ObservableCollection<VyazkaListItemViewModel> VyazkaModelList
        {
            get { return GetField<ObservableCollection<VyazkaListItemViewModel>>(); }
            set { SetField(value); }
        }

        public VyazkaListViewModel()
        {
            VyazkaModelList = new ObservableCollection<VyazkaListItemViewModel>();

           
        }
    }
}
