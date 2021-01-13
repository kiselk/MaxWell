using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using MaxWell.Helpers;
using MaxWell.Models;
using MaxWell.Services;
using MaxWell.ViewModels.Comments;
using MaxWell.ViewModels.Foods;
using MaxWell.ViewModels.Persons;
using MaxWell.ViewModels.Prides;
using Xamarin.Forms;
using Xamvvm;
using RemoteImage = MaxWell.Models.RemoteImage;

namespace MaxWell.ViewModels.Foods
{
    public class FoodDetailViewModel : BaseViewModel
    {

        public FoodDetailViewModel()
        {
        }

        public FoodDetailViewModel(Food vyazka)
        {
            this.Food = vyazka;
            
            PrideModelList = new ObservableCollection<PrideListItemViewModel>();
         
           CatModelList = new ObservableCollection<CatListItemViewModel>();
          
           KittenModelList = new ObservableCollection<CatListItemViewModel>();
            CommentByModelList= new ObservableCollection<CommentListItemViewModel>();
            CommentForModelList = new ObservableCollection<CommentListItemViewModel>();
            /*  */
            SomeImage = new RemoteImage();
        }



        private Food _vyazka;

        public Food Food
        {
            get => _vyazka;
            set { SetProperty(ref _vyazka, value); }
        }



        private ObservableCollection<PrideListItemViewModel> _prideModelList;
        public ObservableCollection<PrideListItemViewModel> PrideModelList
        {
            get => _prideModelList;
            set { SetProperty(ref _prideModelList, value); }
        }

        private ObservableCollection<CatListItemViewModel> _catModelList;
        public ObservableCollection<CatListItemViewModel> CatModelList
        {
            get => _catModelList;
            set { SetProperty(ref _catModelList, value); }
        }
        private ObservableCollection<CatListItemViewModel> _kittenModelList;
        public ObservableCollection<CatListItemViewModel> KittenModelList
        {
            get => _kittenModelList;
            set { SetProperty(ref _kittenModelList, value); }
        }
        private ObservableCollection<CommentListItemViewModel> _commentByModelList;
        public ObservableCollection<CommentListItemViewModel> CommentByModelList
        {
            get => _commentByModelList;
            set { SetProperty(ref _commentByModelList, value); }
        }
        private ObservableCollection<CommentListItemViewModel> _commentForModelList;
        public ObservableCollection<CommentListItemViewModel> CommentForModelList
        {
            get => _commentForModelList;
            set { SetProperty(ref _commentForModelList, value); }
        }
        private RemoteImage _someImage;
        public RemoteImage SomeImage
        {
            get => _someImage;
            set { SetProperty(ref _someImage, value); }
        }
     
    }
}