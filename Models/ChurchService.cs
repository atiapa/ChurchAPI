using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChurchApi.Models
{
    public class ChurchService
    {
        [Key]
        public int Refno { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public int ChurchID { get; set; }
        public string EntryID { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
