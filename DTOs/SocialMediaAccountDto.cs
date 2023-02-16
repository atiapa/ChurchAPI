using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChurchApi.Services;

namespace ChurchApi.DTOs
{
    public class SocialMediaAccountDto : LookUpDto
    {
        public string Icon { get; set; }
        public string Address { get; set; }
    }
}
