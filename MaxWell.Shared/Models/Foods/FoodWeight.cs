using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using SQLite;
using Newtonsoft.Json;
using Xamarin.Forms;


namespace MaxWell.Models.Foods
{
    public class FoodWeight
    {
        [PrimaryKey, AutoIncrement] public int Id { get; set; }
         public int FoodId { get; set; }
       public double Amount { get; set; }
        public string MeasurementEn { get; set; }
        public string Measurement { get; set; }
        public double? Unknown1 { get; set; }
        public string Unknown2 { get; set; }
        public string Unknown3 { get; set; }

    }
}
