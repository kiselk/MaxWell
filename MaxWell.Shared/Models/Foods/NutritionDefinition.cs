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
    public class NutritionDefinition
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string MeasurementEn { get; set; }
        public string Measurement { get; set; }
        public string Abbreviation { get; set; }
        public string NameEn { get; set; }
        public string Name { get; set; }
        public int? Unknown1 { get; set; }
        public int? Unknown2 { get; set; }
        public  IList<NutritionData> NutritionDataCollection { get; set; }

    }
}
