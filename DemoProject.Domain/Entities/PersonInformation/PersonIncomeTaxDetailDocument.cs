using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonIncomeTaxDetailDocument")]
    public partial class PersonIncomeTaxDetailDocument
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonIncomeTaxDetailDocument()
        {
            PersonIncomeTaxDetailDocumentMakerCheckers = new HashSet <PersonIncomeTaxDetailDocumentMakerChecker> ();
        }

        [Key]
        public long PrmKey { get; set; }

        public long PersonIncomeTaxDetailPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

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

        public virtual PersonIncomeTaxDetail PersonIncomeTaxDetail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonIncomeTaxDetailDocumentMakerChecker> PersonIncomeTaxDetailDocumentMakerCheckers { get; set; }
    }
}
