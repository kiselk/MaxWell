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
using MaxWell.ViewModels.Prides;
using Xamarin.Forms;
using Xamvvm;
using RemoteImage = MaxWell.Models.RemoteImage;

namespace MaxWell.ViewModels.Persons
{
    public class PersonDetailViewModel : BaseViewModel
    {

        public PersonDetailViewModel()
        {
        }

        public PersonDetailViewModel(Person person)
        {
            this.Person = person;
            
            FoodModelList = new ObservableCollection<FoodListItemViewModel>();
            CommentByModelList= new ObservableCollection<CommentListItemViewModel>();
            CommentForModelList = new ObservableCollection<CommentListItemViewModel>();
         
        }
        private Person _person;

        public Person Person
        {
            get => _person;
            set { SetProperty(ref _person, value); }
        }

        private ObservableCollection<FoodListItemViewModel> _foodModelList;
        public ObservableCollection<FoodListItemViewModel> FoodModelList
        {
            get => _foodModelList;
            set { SetProperty(ref _foodModelList, value); }
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
     
     
    }
}