using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonPhotoSign")]
    public partial class PersonPhotoSign
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonPhotoSign()
        {
            PersonPhotoSignMakerCheckers = new HashSet<PersonPhotoSignMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        //public Guid PersonPhotoSignId { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [Required]
        [StringLength(500)]
        public string PhotoNameOfFile { get; set; }

        [Required]
        [StringLength(500)]
        public string PhotoFileCaption { get; set; }

        [Required]
        public byte[] PhotoCopy { get; set; }

        [Required]
        [StringLength(1500)]
        public string PhotoLocalStoragePath { get; set; }

        [Required]
        [StringLength(500)]
        public string SignNameOfFile { get; set; }

        [Required]
        [StringLength(500)]
        public string SignFileCaption { get; set; }

        [Required]
        public byte[] PersonSign { get; set; }

        [Required]
        [StringLength(1500)]
        public string SignLocalStoragePath { get; set; }

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
        public virtual ICollection<PersonPhotoSignMakerChecker> PersonPhotoSignMakerCheckers { get; set; }
    }
}
