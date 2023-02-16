using ChurchApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ChurchApi.DTOs
{
    public class MemberRegistrationDto : MemberRegistration
    {
        public IFormFile File { get; set; }
    }

    public class SearchFilter
    {
        [FromQuery] 
        public int MemberID { get; set; }
        [FromQuery] 
        public string DOB { get; set; }
        [FromQuery]
        public string FirstName { get; set; }
    }
      
    public class GetMembersListDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MemberID { get; set; }
    }


}
