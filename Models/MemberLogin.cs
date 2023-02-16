using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChurchApi.Models
{
    public class MemberLogin
    {
        [Key]
        public int MemberID { get; set; }
        [Required]
        public string FirstName { get; set; }
        
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; } 
        
        public string Email { get; set; }
        public string DOB { get; set; }
        [Required]
        public int ChurchID { get; set; }
       
        public string Username { get; set; }

   

        


    }
}
