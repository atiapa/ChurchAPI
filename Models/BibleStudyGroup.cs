using System;
using System.ComponentModel.DataAnnotations;

namespace ChurchApi.Models
{
    public class BibleStudyGroup 
    {

        [Key]
        public int Refno { get; set; }
        public string Groups { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public int ChurchID { get; set; }
        public string entryID { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
