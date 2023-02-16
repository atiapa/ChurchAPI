using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace ChurchApi.Helpers
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public AllowedExtensionsAttribute(string[] extensions) => _extensions = extensions;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var file = value as IFormFile;
                var extension = Path.GetExtension(file.FileName);
                if (file != null)
                {
                    if (!_extensions.Contains(extension.ToLower()))
                    {
                        return new ValidationResult("Filetype is not supported.");
                    }
                }
                return ValidationResult.Success;
            }
            catch (Exception)
            {
                return new ValidationResult("Unable to ready file type.");
            }
        }
    }
}
