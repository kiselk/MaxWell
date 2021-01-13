using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using SQLite;
using Newtonsoft.Json;
using Xamarin.Forms;


namespace MaxWell.Models.Foods
{
    public class FoodDescription
    {
        [PrimaryKey, AutoIncrement]
        public int FoodDescriptionId { get; set; }
        public int FoodGroupId { get; set; }
        public string NameEn { get; set; }
        public string NameEnUC { get; set; }
        public string Name { get; set; }
        public string Unknown1 { get; set; }
        public string Unknown2 { get; set; }
        public string Unknown3 { get; set; }
        public string Unknown4 { get; set; }
        public double? Unknown5 { get; set; }
        public string Unknown6 { get; set; }
        public double? Unknown7 { get; set; }
        public double? Unknown8 { get; set; }
        public double? Unknown9 { get; set; }
        public double? Unknown10 { get; set; }
        public double? Phenyl { get; set; }


         public IList<NutritionData> NutritionDataCollection { get; set; }

        [JsonIgnore]
         public double? PhenylAmount => GetPhenylAmount();


        public double? GetPhenylAmount()
        {
            double? phenylAmount = 0.0;
            try
            {
                if (NutritionDataCollection == null)
                    return null;

                 phenylAmount = NutritionDataCollection.FirstOrDefault(d => d.NutritionDefinitionId.Equals(508)).Amount1;
            }
            catch (Exception e)
            {

                return null;
            }

            return phenylAmount;
        }
    }
}
