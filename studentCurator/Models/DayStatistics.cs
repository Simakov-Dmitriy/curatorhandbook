using System;

namespace studentCurator.Models
{
    public class DayStatistics
    {
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public int GroupId { get; set; }
        public string Statistics { get; set; }
    }
}