using Microsoft.AspNetCore.Identity;
using OmatrackApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChurchApi.Models
{
    public class User : IdentityUser
    {
        [MaxLength(60), Required]
        public string Name { get; set; }
        //[MaxLength(60), Required]
        //public string FirstName { get; set; }
        //public string Picture { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public bool Locked { get; set; }
        public bool Hidden { get; set; }
        public View View { get; set; }
        public virtual Profile Profile { get; set; }
        public long ProfileId { get; set; }
        //public virtual Bank Bank{ get; set;}
        //public long? BankId { get; set; }
    }

    public enum View
    {
        Admin,
        Portal
    }
}
