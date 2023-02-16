using Microsoft.AspNetCore.Http;
using ChurchApi.Helpers;
using ChurchApi.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace ChurchApi.DTOs
{
    public class DocumentDto
    {
        //[Required]
        public long Id { get; set; }
        public long? ReferenceId { get; set; }
        public string FileName { get; set; }
        public string FileLabel { get; set; }
        [StringLength(128)]
        public string Description { get; set; }
        public string RootPath { get; set; }
        [AllowedExtensions(new string[] { ".xls", ".xlsx" })]
        public IFormFile File { get; set; }
        public string Username { get; set; }
        public string User { get; set; }
        public bool IsTemplate { get; set; }
        //public long? BankId { get; set; }
    }

    public class DocumentCardDto
    {
        public long Id { get; set; }
        public string Documentation { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Username { get; set; }
        public string User { get; set; }
        public DateTime Timestamp { get; set; }
        public bool CanDelete { get; set; }
        public bool IsTemplate { get; set; }
        //public string Bank { get; set; }
        public DocumentStatus Status { get; set; }
        public long? ReferenceId { get; set; }
    }
}
