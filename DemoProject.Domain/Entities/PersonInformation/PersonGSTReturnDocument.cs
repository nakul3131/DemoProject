using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonGSTReturnDocument")]
    public partial class PersonGSTReturnDocument
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonGSTReturnDocument()
        {
            PersonGSTReturnDocumentMakerCheckers = new HashSet<PersonGSTReturnDocumentMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        public long PersonGSTRegistrationDetailPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short AssessmentYear { get; set; }

        public decimal TaxAmount { get; set; }

        [Required]
        [StringLength(500)]
        public string NameOfFile { get; set; }

        [Required]
        [StringLength(500)]
        public string FileCaption { get; set; }

        [Required]
        public byte[] PhotoCopy { get; set; }

        [Required]
        [StringLength(1500)]
        public string LocalStoragePath { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual PersonGSTRegistrationDetail PersonGSTRegistrationDetail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonGSTReturnDocumentMakerChecker> PersonGSTReturnDocumentMakerCheckers { get; set; }
    }
}
