using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using MaxWell.Helpers;
using MaxWell.Models;
using MaxWell.Services;
using MaxWell.ViewModels.Foods;
using Xamarin.Forms;

namespace MaxWell.ViewModels.Foods
{
    public class FoodListItemViewModel : BaseViewModel
    {

        public Food Food;
 
    
        public FoodListItemViewModel(Food food)
        {
            Food = food;
       

        }

        public string VKUserId => Food.VKUserId;

     //   public string PlanId => Food.PlanId;

        
        public double? Protein_g => Food.Protein_g;

        public double? Fats_g => Food.Fats_g;

        public double? Carbs_g => Food.Carbs_g;
        public double? Calcium_mg => Food.Calcium_mg;

        public double? Phenylalanine_g => Food.Phenylalanine_g;
        public int? FoodDescriptionId => Food.FoodDescriptionId;

        public string Name => Food.Name;
        public string NameEn => Food.NameEn;

        public string Description => Food.Description;
        public ImageSource ImageAsImageStream => GetImageSource();
        public bool IsSafe => GetIsSafe();
        public ImageSource GetImageSource()
        {
            if(Food.image.Equals(null))
                if (!Food.Description.Equals(""))  
                    Food.image = ImageHelper.ImageUrlToByteArray(Food.Description);
            return Food.ImageAsImageStream;
        }
        public bool GetIsSafe()
        {
            if (!Food.Protein_g.Equals(null))
                if (!Food.Phenylalanine_g.Equals(null))
                    if (Food.Phenylalanine_g < 0.01)
                        return true;
            if (Food.Phenylalanine_g < 0.1)
                return true;
            return false;
        }
    }
}