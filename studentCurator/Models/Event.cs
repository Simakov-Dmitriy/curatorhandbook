using System;

namespace studentCurator.Models
{
    public class Event
    {
        public int Id { get; set; }
        public  DateTime EventDate { get; set; }
        public string Description { get; set; }
        public int GroupId { get; set; }
        
    }
}