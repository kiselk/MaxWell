using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MaxWell.Models
{
    public class Pomet
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime PometDate { get; set; }
        public DateTime PometEndDate { get; set; }
        public int PrideId { get; set; }
        public int MotherId { get; set; }
        public int FatherId { get; set; }
        public string Mother { get; set; }
        public string Father { get; set; }

        public int PregnancyId { get; set; }
        public int PersonId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
