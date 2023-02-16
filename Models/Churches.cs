using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChurchApi.Models
{
    public class Churches
    {

       [Key]
        public int  ChurchID { get; set; }
    
        public string name { get; set; }
        public string branchName { get; set; }
        public string smtpemail { get; set; }
        public string smtppassword { get; set; }
        public string smtpserver { get; set; }
        public int smtpport { get; set; }      

    }
}
