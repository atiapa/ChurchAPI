using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChurchApi.Models
{
    public class RSSFeed
    {
        public string Title { get; set; }
        public string LogoUrl { get; set; }
        public string Source { get; set; }
        public string Category { get; set; }
        public string Summary { get; set; }
        public string Contributors { get; set; }
        public string ImageUrl { get; set; }
        public string Link { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
