using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonKYCDocument")]
    public partial class PersonKYCDocument
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonKYCDocument()
        {
            PersonKYCDocumentMakerCheckers = new HashSet<PersonKYCDocumentMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        public short DocumentDocumentTypePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [Required]
        [StringLength(150)]
        public string NameAsPerDocument { get; set; }

        [Required]
        [StringLength(50)]
        public string DocumentNumber { get; set; }

        public short SequenceNumber { get; set; }

        public DateTime DateOfIssue { get; set; }

        public DateTime? DateOfExpiry { get; set; }

        [Required]
        [StringLength(100)]
        public string IssuingAuthority { get; set; }

        [Required]
        [StringLength(100)]
        public string PlaceOfIssue { get; set; }

        [Required]
        [StringLength(500)]
        public string NameOfFile { get; set; }

        [Required]
        [StringLength(500)]
        public string FileCaption { get; set; }

        [Required]
        public byte[] DocumentPhotoCopy { get; set; }

        [Required]
        [StringLength(1500)]
        public string LocalStoragePath { get; set; }

        public DateTime? DateOfRequest { get; set; }

        public DateTime? DateOfExpectingSubmit { get; set; }

        public DateTime? DateOfSubmit { get; set; }

        [Required]
        [StringLength(1)]
        public string DocumentUploadStatus { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Person Person { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonKYCDocumentMakerChecker> PersonKYCDocumentMakerCheckers { get; set; }
    }
}
