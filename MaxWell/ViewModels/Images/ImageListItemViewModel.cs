using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using MaxWell.Helpers;
using MaxWell.Models;
using MaxWell.Models.Foods;
using MaxWell.Services;
using MaxWell.ViewModels.Foods;
using Xamarin.Forms;

namespace MaxWell.ViewModels.Images
{
    public class ImageListItemViewModel : BaseViewModel
    {

    

        public ImageListItemViewModel(Food food,FoodDescription foodDescription, string url)
        {
            FoodDescription = foodDescription;
            Food = food;
            ImageUrl = url;
        }

        public string ImageUrl { get; set; }
    public FoodDescription FoodDescription { get; set; }
        public Food Food { get; set; }
        

    }
}