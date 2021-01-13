using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using SQLite;
using Newtonsoft.Json;
using Xamarin.Forms;


namespace MaxWell.Models.Foods
{
    [JsonObject(IsReference = true)]
    public class NutritionData
    {
        [PrimaryKey, AutoIncrement]
        public int NutritionDataId { get; set; }
        public int FoodDescriptionId { get; set; }
        public int NutritionDefinitionId { get; set; }
        public double? Amount1 { get; set; }
        public double? Amount2 { get; set; }
        public double? Amount3 { get; set; }
        public double? Amount4 { get; set; }
        public string Amount5 { get; set; }
        public string Amount6 { get; set; }
        public string Amount7 { get; set; }
        public string Amount8 { get; set; }
        public string Amount9 { get; set; }
        public string Amount10 { get; set; }
        public string Amount11 { get; set; }
        public string Amount12 { get; set; }
        public string Amount13 { get; set; }
        public string Amount14 { get; set; }
        public string Amount15 { get; set; }
        public  FoodDescription FoodDescription { get; set; }
    public  NutritionDefinition NutritionDefinition { get; set; }
    }
}
