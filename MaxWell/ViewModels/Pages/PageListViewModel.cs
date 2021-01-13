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

namespace MaxWell.ViewModels.Pages
{
    class PageListViewModel : BasePageModel

    {
        public ObservableCollection<PageListItemViewModel> PageModelList
        {
            get { return GetField<ObservableCollection<PageListItemViewModel>>(); }
            set { SetField(value); }
        }

        public PageListViewModel()
        {
            PageModelList = new ObservableCollection<PageListItemViewModel>();


        }
    }
}
