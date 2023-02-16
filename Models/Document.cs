using System;

namespace ChurchApi.Models
{
    public class Document:AuditFields
    {
        public string FileName { get; set; }
        public string FileLabel { get; set; }
        public string Description { get; set; }
        public DocumentType DocumentType { get; set; }
        public DocumentStatus DocumentStatus { get; set; }
        public DateTime Timestamp { get; set; }
        public string RootPath { get; set; }
        public long? ReferenceId { get; set; }
        public bool IsTemplate { get; set; }
        public virtual Bank Bank { get; set; }
        public long? BankId { get; set; }
    }

    public enum DocumentType
    {
        Excel,
        Word,
        PDF
    }

    public enum DocumentStatus
    {
        Pending,
        Complete
    }
}
