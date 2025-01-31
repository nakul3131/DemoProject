using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonMachineryAssetDocument")]
    public partial class PersonMachineryAssetDocument
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonMachineryAssetDocument()
        {
            PersonMachineryAssetDocumentMakerCheckers = new HashSet<PersonMachineryAssetDocumentMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        //public Guid PersonMachineryAssetDocumentId { get; set; }

        public long PersonMachineryAssetPrmKey { get; set; }

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

        public virtual PersonMachineryAsset PersonMachineryAsset { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonMachineryAssetDocumentMakerChecker> PersonMachineryAssetDocumentMakerCheckers { get; set; }
    }
}
