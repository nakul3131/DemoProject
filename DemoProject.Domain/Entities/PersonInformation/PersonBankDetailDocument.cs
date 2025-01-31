using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonBankDetailDocument")]
    public partial class PersonBankDetailDocument
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonBankDetailDocument()
        {
            PersonBankDetailDocumentMakerCheckers = new HashSet<PersonBankDetailDocumentMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        public long PersonBankDetailPrmKey { get; set; }

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

        public virtual PersonBankDetail PersonBankDetail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonBankDetailDocumentMakerChecker> PersonBankDetailDocumentMakerCheckers { get; set; }
    }
}
