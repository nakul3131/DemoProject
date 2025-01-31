using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonFamilyDetail")]
    public partial class PersonFamilyDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonFamilyDetail()
        {
            PersonFamilyDetailMakerCheckers = new HashSet<PersonFamilyDetailMakerChecker>();
            PersonFamilyDetailTranslations = new HashSet<PersonFamilyDetailTranslation>();
        }

        [Key]
        public long PrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public long PersonInformationNumber { get; set; }

        [Required]
        [StringLength(150)]
        public string FullNameOfFamilyMember { get; set; }

        public byte RelationPrmKey { get; set; }

        public DateTime BirthDate { get; set; }

        public short OccupationPrmKey { get; set; }

        public decimal Income { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonFamilyDetailMakerChecker> PersonFamilyDetailMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonFamilyDetailTranslation> PersonFamilyDetailTranslations { get; set; }
    }
}
