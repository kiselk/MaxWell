using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using SQLite;
using Newtonsoft.Json;
using Xamarin.Forms;


namespace MaxWell.Models.Foods
{
    public class FoodGroup
    {
        [PrimaryKey, AutoIncrement]
        public int FoodGroupId { get; set; }
        public string NameEn { get; set; }
        public string Name { get; set; }

    }
}
