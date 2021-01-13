using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using MaxWell.Helpers;
using MaxWell.Models;
using MaxWell.Services;
using MaxWell.ViewModels.Foods;
using Xamarin.Forms;

namespace MaxWell.ViewModels.Persons
{
    public class PersonListItemViewModel : BaseViewModel
    {

        public Person Person;

        public List<Food> foods;

        public PersonListItemViewModel(Person person)
        {
            Person = person;
            FoodModelList = new ObservableCollection<FoodListItemViewModel>();
            FoodModelList.Clear();
            foods = new List<Food>();

       if (person != null)
            {
            }
        }

        public async Task LoadFoods()
        {
           foods = await App.FoodManager.GetFoodsByVkAsync(Person.VKUserId.ToString());
            int i = 0;
            foreach (Food food in foods)
            {
                if (i > 5) break;
                FoodModelList.Add(new FoodListItemViewModel(food));
                i++;
            }
          
        }

        private ObservableCollection<FoodListItemViewModel> _foodModelList;
        public ObservableCollection<FoodListItemViewModel> FoodModelList
        {
            get => _foodModelList;
            set { SetProperty(ref _foodModelList, value); }
        }

        public DateTime Birthday => Person.Birthday;
        public string Name => Person.Name;
        public int FoodsCount => foods.Count;
        public int TodoCount => foods.Count;
        public string Description => Person.Description;
        public string ImageUrl => Person.ImageUrl;
        public ImageSource ImageAsImageStream => Person.ImageAsImageStream;

    }
}