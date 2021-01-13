using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using SQLite;
using Newtonsoft.Json;
using Xamarin.Forms;
namespace MaxWell.Models
{
    public class Vyazka
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTime SexDate { get; set; }

        public int MotherId { get; set; }
        public int FatherId { get; set; }
        public string Mother { get; set; }
        public string Father { get; set; }

        public int PregnancyId { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }





   
    }
}
