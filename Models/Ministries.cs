using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChurchApi.Models
{
    public class Ministries
    {
        [Key]
        public int Refno { get; set; }
        public string Type { get; set; }
        public string Ministry { get; set; }
        public string Note { get; set; }
        public int ChurchID { get; set; }
        public string entryID { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
