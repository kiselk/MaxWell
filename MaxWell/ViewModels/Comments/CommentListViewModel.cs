using MaxWell.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows.Input;
using MaxWell.Helpers;
using MaxWell.Services;
using Xamarin.Forms;
using Xamvvm;

namespace MaxWell.ViewModels.Comments
{
    class CommentListViewModel : BasePageModel
        {

            public ObservableCollection<CommentListItemViewModel> CommentModelList
            {
                get { return GetField<ObservableCollection<CommentListItemViewModel>>(); }
                set { SetField(value); }
            }

            public CommentListViewModel()
            {
                CommentModelList = new ObservableCollection<CommentListItemViewModel>();


            }
        }
    
}
