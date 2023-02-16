using System;

namespace ChurchApi.Models
{
    public class Stat:HasId
    {
        public DateTime TimeStamp { get; set; }
        public int Downloads { get; set; }
        public int Uploads { get; set; }
    }
}
