using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MaxWell.Models
{
    public class Pregnancy
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime PregnancyDate { get; set; }
        public DateTime PregnancyEndDate { get; set; }

        public int MotherId { get; set; }
        public int FatherId { get; set; }
        public string Mother { get; set; }
        public string Father { get; set; }

        public int PregnancyId { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
