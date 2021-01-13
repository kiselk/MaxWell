using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using SQLite;
using Newtonsoft.Json;
using Xamarin.Forms;


namespace MaxWell.Models
{
    public class FoodInfo
    {
        //Id,Name,Description,Birthday,VK,Cats,Pitomniki,Gender,icon,image
        [PrimaryKey, AutoIncrement]
        public int FoodInfoId { get; set; }



        //Id,Group,Desc,Shrt_Desc,Shrt_Desc_Ru,Water_g,Energ_Kcal,Protein_g,Lipid_Tot_g,Ash_g,Carbohydrt_g,Fiber_TD_g,Sugar_Tot_g,Calcium_mg,Iron_mg,Magnesium_mg,Phosphorus_mg,Potassium_mg,Sodium_mg,Zinc_mg,Copper_mg,Manganese_mg,Selenium_µg,Vit_C_mg,Thiamin_mg,Riboflavin_mg,Niacin_mg,Panto_Acid_mg,Vit_B6_mg,Folate_Tot_ug,Folic_Acid_ug,Food_Folate_ug,Folate_DFE_ug,Choline_Tot_ mg,Vit_B12_ug,Vit_A_IU,Vit_A_RAE,Retinol_ug,Alpha_Carot_ug,Beta_Carot_ug,Beta_Crypt_ug,Lycopene_ug,Lut+Zea_ ug,Vit_E_mg,Vit_D_ug,Vit_D_IU,Vit_K_ug,FA_Sat_g,FA_Mono_g,FA_Poly_g,Cholestrl_mg,GmWt_1,GmWt_Desc1,GmWt_2,GmWt_Desc2,Refuse_Pct,

        public string Group { get; set; }
        public string Desc { get; set; }
        public string Shrt_Desc { get; set; }
        public string Shrt_Desc_Ru { get; set; }
        //int
    
      //  public double Phenylalanine_g { get; set; }
        public double Water_g { get; set; }
        public double Energ_Kcal { get; set; }
        public double Protein_g { get; set; }
        public double Lipid_Tot_g { get; set; }
        public double Ash_g { get; set; }
        public double Carbohydrt_g { get; set; }
        public double Fiber_TD_g { get; set; }
        public double Sugar_Tot_g { get; set; }
        public double Calcium_mg { get; set; } 
        public double Iron_mg { get; set; }
        public double Magnesium_mg { get; set; }
        public double Phosphorus_mg { get; set; }
        public double Potassium_mg { get; set; }
        public double Sodium_mg { get; set; }
        public double Zinc_mg { get; set; }
        public double Copper_mg { get; set; }
        public double Manganese_mg { get; set; }
        public double Selenium_µg { get; set; }
        public double Vit_C_mg { get; set; }
        public double Thiamin_mg { get; set; }
        public double Riboflavin_mg { get; set; }
        public double Niacin_mg { get; set; }
        public double Panto_Acid_mg { get; set; }
        public double Vit_B6_mg { get; set; }
        public double Folate_Tot_ug { get; set; }
        public double Folic_Acid_ug { get; set; }
        public double Food_Folate_ug { get; set; }
        public double Folate_DFE_ug { get; set; }
        public double Choline_Tot_mg { get; set; }
        public double Vit_B12_ug { get; set; }
        public double Vit_A_IU { get; set; }
        public double Vit_A_RAE { get; set; }
        public double Retinol_ug { get; set; }
        public double Alpha_Carot_ug { get; set; }
        public double Beta_Carot_ug { get; set; }
        public double Beta_Crypt_ug { get; set; }
        public double Lycopene_ug { get; set; }
        public double Lut_Zea_ug { get; set; }
        public double Vit_E_mg { get; set; }
        public double Vit_D_ug { get; set; }
        public double Vit_D_IU { get; set; }
        public double Vit_K_ug { get; set; }
        public double FA_Sat_g { get; set; }
        public double FA_Mono_g { get; set; }
        public double FA_Poly_g { get; set; }
        public double Cholestrl_mg { get; set; }
        public double GmWt_1 { get; set; }
        public string GmWt_Desc1 { get; set; }
        public double GmWt_2 { get; set; }
        public string GmWt_Desc2 { get; set; }
        public double Refuse_Pct { get; set; }
        public double Phenylalanine_g { get; set; }
        


        public byte[] icon { get; set; }
        public byte[] image { get; set; }


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
