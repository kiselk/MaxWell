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
using MaxWell.Models.Foods;
using MaxWell.Services;
using MaxWell.ViewModels.Comments;
using MaxWell.ViewModels.Pomets;
using Xamarin.Forms;
using Xamvvm;

namespace MaxWell.ViewModels.Images
{
    class ImageListViewModel : BasePageModel
    {

        public ObservableCollection<ImageListItemViewModel> ImageModelList
        {
            get { return GetField<ObservableCollection<ImageListItemViewModel>>(); }
            set { SetField(value); }
        }
        public FoodDescription FoodDescription
        {
            get { return GetField<FoodDescription>(); }
            set { SetField(value); }
        }


        public Food Food
        {
            get { return GetField<Food>(); }
            set { SetField(value); }
        }

        public ImageListViewModel()
        {
            ImageModelList = new ObservableCollection<ImageListItemViewModel>();
            _source = new ObservableCollection<ImageListItemViewModel>();


        }

        private ObservableCollection<ImageListItemViewModel> _source;
        public ObservableCollection<ImageListItemViewModel> Source
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
           var loading = UserDialogs.Instance.Loading("Изображение", null, null, true);
            try
            {
                var urls = await GoogleService.getInstance().getImages(FoodDescription.Name);//await App.FoodManager.GetFoodsAsync();
             //   UserDialogs.Instance.AlertAsync(""+foods.Count, "" + this.GetType() + " foods");
              //  viewModel = (RestFoodListViewModel)BindingContext;
                ImageModelList.Clear();
                foreach (var url in urls.Photos)
                {
              //      UserDialogs.Instance.AlertAsync(food.Name, "" + this.GetType() + " food");
                    ImageModelList.Add(new ImageListItemViewModel(Food, FoodDescription,url));
                }
               
            }
            catch (Exception e)
            {
                UserDialogs.Instance.AlertAsync(e.Message,""+this.GetType() + " List Error");
            }
            loading.Hide();

        }
        
    }

}
