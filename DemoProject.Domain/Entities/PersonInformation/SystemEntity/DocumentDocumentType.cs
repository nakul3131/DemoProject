using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation.SystemEntity
{
    [Table("DocumentDocumentType")]
    public partial class DocumentDocumentType
    {
        [Key]
        public short PrmKey { get; set; }

        public Guid DocumentDocumentTypeId { get; set; }

        public byte DocumentTypePrmKey { get; set; }

        public short DocumentPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Document Document { get; set; }

        public virtual DocumentType DocumentType { get; set; }
        
    }
}
