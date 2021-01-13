using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using SQLite;
using Newtonsoft.Json;
using Xamarin.Forms;
using MaxWell.Models.Foods;
using MaxWell.Shared.Models.Foods.Plans;
using MaxWell.Shared.Models.Foods;
namespace MaxWell.Models
{
   public class Food
    {
        [PrimaryKey, AutoIncrement]
        //Id,Name,Description,Birthday,VK,Cats,Pitomniki,Gender,icon,image
        public int FoodId { get; set; }
         public string Name { get; set; }
        public string NameEn { get; set; }
        public string Description { get; set; }
      
        public string VKUserId { get; set; }


        
        public double? Protein_g { get; set; }
 
        public double? Fats_g { get; set; }
     
        public double? Carbs_g { get; set; }
        public double? Calcium_mg { get; set; }

        public double? Phenylalanine_g { get; set; }
        public int? FoodDescriptionId { get; set; }
        
        public byte[] icon { get; set; }
        public byte[] image { get; set; }
     //   public List<NutritionData> NutritionDataCollection { get; set; }

        [JsonIgnore]
        public ImageSource IconAsImageStream => GetIconStream();
        [JsonIgnore]
        public ImageSource ImageAsImageStream => GetImageStream();
      

        public ImageSource GetImageStream()
        {
            try
            {
                if (image == null)
                    return null;

                var imageByteArray = image;

                return ImageSource.FromStream(() => new MemoryStream(imageByteArray));
            }
            catch (Exception e)
            {
            
                return null;
            }
        }

        public ImageSource GetIconStream()
        {
            try
            {
                if (icon == null)
                    return null;

                var imageByteArray = icon;

                return ImageSource.FromStream(() => new MemoryStream(imageByteArray));
            }
            catch (Exception e)
            {
               
                return null;
            }
        }
     
}
}
