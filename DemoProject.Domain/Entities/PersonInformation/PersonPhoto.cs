using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonPhoto")]
    public partial class PersonPhoto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonPhoto()
        {
            PersonPhotoMakerCheckers = new HashSet<PersonPhotoMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        //public Guid PersonPhotoDetailId { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [Required]
        public byte[] Photo { get; set; }

        [Required]
        [StringLength(4000)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Person Person { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonPhotoMakerChecker> PersonPhotoMakerCheckers { get; set; }
    }
}
