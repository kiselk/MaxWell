using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using SQLite;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MaxWell.Shared.Models
{
   public class Allergen
    {
        [PrimaryKey, AutoIncrement]
        public int AllergenId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateDateTime { get; set; }
      
        public int PersonId { get; set; }
        public string ImageUrl { get; set; }

     
}
}
