using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonAddressTranslation")]
    public partial class PersonAddressTranslation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonAddressTranslation()
        {
            PersonAddressTranslationMakerCheckers = new HashSet<PersonAddressTranslationMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        //public Guid PersonAddressTranslationId { get; set; }

        public long PersonAddressPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string TransFlatDoorNo { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfBuilding { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfRoad { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfArea { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual PersonAddress PersonAddress { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonAddressTranslationMakerChecker> PersonAddressTranslationMakerCheckers { get; set; }
    }
}
