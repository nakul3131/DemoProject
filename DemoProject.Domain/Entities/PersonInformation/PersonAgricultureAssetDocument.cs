using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonAgricultureAssetDocument")]
    public partial class PersonAgricultureAssetDocument
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonAgricultureAssetDocument()
        {
            PersonAgricultureAssetDocumentMakerCheckers = new HashSet<PersonAgricultureAssetDocumentMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        //public Guid PersonAgricultureAssetDocumentId { get; set; }

        public long PersonAgricultureAssetPrmKey { get; set; }

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

        public virtual PersonAgricultureAsset PersonAgricultureAsset { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonAgricultureAssetDocumentMakerChecker> PersonAgricultureAssetDocumentMakerCheckers { get; set; }
    }
}
