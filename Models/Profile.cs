using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ChurchApi.Models;

namespace OmatrackApi.Models
{
    public class Profile:HasId
    {
        [Required,MaxLength(512)]
        public string Name { get; set; }
        public string DefaultView { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        public string Privileges { get; set; }
        public bool Locked { get; set; }
        public bool Hidden { get; set; }
        public virtual List<User> Users { get; set; }
    }

    public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.HasIndex(q => q.Name).IsUnique();
        }
    }

}
