using DemoProject.Domain.Entities.PersonInformation.SystemEntity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeDocument")]
    public partial class SchemeDocument
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeDocument()
        {
            SchemeDocumentMakerCheckers = new HashSet<SchemeDocumentMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public short DocumentPrmKey { get; set; }

        public bool IsRequired { get; set; }

        public bool EnableDocumentUploadInDb { get; set; }

        [Required]
        [StringLength(500)]
        public string DocumentAllowedFileFormatsForDb { get; set; }

        public short MaximumFileSizeForDocumentUploadInDb { get; set; }

        public bool EnableDocumentUploadInLocalStorage { get; set; }

        [Required]
        [StringLength(1500)]
        public string DocumentLocalStoragePath { get; set; }

        [Required]
        [StringLength(500)]
        public string DocumentAllowedFileFormatsForLocalStorage { get; set; }

        public short MaximumFileSizeForDocumentUploadInLocalStorage { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Scheme Scheme { get; set; }

        public virtual Document Document { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeDocumentMakerChecker> SchemeDocumentMakerCheckers { get; set; }
    }
}
