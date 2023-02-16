using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChurchApi.Models
{
    public interface IHasId
    {
        [Key]
        long Id { get; set; }
    }

    public interface ISecured
    {
        bool Locked { get; set; }
        bool Hidden { get; set; }
    }

    public interface IAuditable : IHasId
    {
        [Required]
        string CreatedBy { get; set; }
        [Required]
        string ModifiedBy { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime ModifiedAt { get; set; }
    }

    public class HasId : IHasId
    {
        public long Id { get; set; }
    }

    public class AuditFields : HasId, IAuditable
    {
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public string ModifiedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }

    public class LookUp : HasId
    {
        [MaxLength(60),Required]
        public string Name { get; set; }
        [MaxLength(512)]
        public string Description { get; set; }
    }
}
