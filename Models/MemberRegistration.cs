using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel.DataAnnotations;
//services.AddControllers().AddNewtonsoftJson();
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace ChurchApi.Models
{
    public class MemberRegistration
    {
        [Key]
        public int MemberID { get; set; }
        public int ChurchID { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public int Age { get; set; }
        public string MaritalStatus { get; set; }
        public string ResidentialAddress { get; set; }
        public string Landmark { get; set; }
        public string DigitalAddress { get; set; }
        public string PostalAddress { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string MemberStatus { get; set; }
        public string Employment { get; set; }
        public string Occupation { get; set; }
        public string AcademicLevel { get; set; }
        public string Service { get; set; }
        public string ChurchGroups { get; set; }
        public string PositionInChurch { get; set; }
        public string BibleStudyGroup { get; set; }
        public string HomeCellGroup { get; set; }
        public byte[] Photo { get; set; }
        public string Status { get; set; }
        public string Inactive_Reason { get; set; }
        public string Baptized { get; set; }
        public string BaptizmaDate { get; set; }
        public string MarriageDate { get; set; }
        public string EntryDate { get; set; }
        public string EntryID { get; set; }
        public string ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string FPTemplate { get; set; }
        public string Remarks { get; set; }
        public string Communicant { get; set; }
        public string HolySpiritBaptism { get; set; }
        public string Transfered { get; set; }
        public string TransferedDate { get; set; }
        public string TransferedToFrom { get; set; }
        public string Deceased { get; set; }
        public string Dateofdeath { get; set; }
        public string Officer { get; set; }
        public string DependantID { get; set; } 
        public string Relation { get; set; }
        public string MemberApproved { get; set; }
    }
}
