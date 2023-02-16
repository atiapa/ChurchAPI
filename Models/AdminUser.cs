using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;



namespace ChurchApi.Models
{
    public class AdminUser
    {
        [Key]
        public int User_ID { get; set; }
        [Required]
        public int MemberID { get; set; } 
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Access_level { get; set; }
        [Required]
        public string Username { get; set; }
        public string Password { get; set; }
        public int ChurchID { get; set; }

    }
}
