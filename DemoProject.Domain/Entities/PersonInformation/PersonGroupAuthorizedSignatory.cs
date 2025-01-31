using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonGroupAuthorizedSignatory")]
    public partial class PersonGroupAuthorizedSignatory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonGroupAuthorizedSignatory()
        {
            PersonGroupAuthorizedSignatoryMakerCheckers = new HashSet<PersonGroupAuthorizedSignatoryMakerChecker>();
            PersonGroupAuthorizedSignatoryTranslations = new HashSet<PersonGroupAuthorizedSignatoryTranslation>();
        }

        [Key]
        public long PrmKey { get; set; }

        public long PersonGroupPrmKey { get; set; }

        public short DesignationPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public long PersonInformationNumber { get; set; }

        [Required]
        [StringLength(150)]
        public string FullNameOfAuthorizedPerson { get; set; }

        [Required]
        [StringLength(1500)]
        public string AuthorizedPersonAddressDetail { get; set; }

        [Required]
        [StringLength(1500)]
        public string AuthorizedPersonContactDetail { get; set; }


        public bool IsAuthorizedSignatory { get; set; }

        [Required]
        [StringLength(150)]
        public string SignNameOfFile { get; set; }

        [Required]
        [StringLength(150)]
        public string SignFileCaption { get; set; }

        public byte[] Sign { get; set; }

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

        public virtual PersonGroup PersonGroup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonGroupAuthorizedSignatoryMakerChecker> PersonGroupAuthorizedSignatoryMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonGroupAuthorizedSignatoryTranslation> PersonGroupAuthorizedSignatoryTranslations { get; set; }
    }
}
