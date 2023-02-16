using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChurchApi.Models
{
    public class Picture:HasId
    {
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
    }
}
