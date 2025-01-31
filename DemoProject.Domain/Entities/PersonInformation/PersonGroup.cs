using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonGroup")]
    public partial class PersonGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonGroup()
        {
            PersonGroupMakerCheckers = new HashSet<PersonGroupMakerChecker>();
            PersonGroupAuthorizedSignatories = new HashSet<PersonGroupAuthorizedSignatory>();
        }

        [Key]
        public long PrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [Required]
        [StringLength(3)]
        public string BusinessType { get; set; }

        [Required]
        [StringLength(3)]
        public string BusinessNature { get; set; }

        public DateTime DateOfEstablishment { get; set; }

        [Required]
        [StringLength(150)]
        public string BusinessRegistrationNumber { get; set; }

        [Required]
        [StringLength(150)]
        public string OtherRegistrationNumber { get; set; }

        public bool HasAnyAssociatedCompanies { get; set; }

        [Required]
        [StringLength(1500)]
        public string AssociatedCompaniesList { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Person Person { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonGroupMakerChecker> PersonGroupMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonGroupAuthorizedSignatory> PersonGroupAuthorizedSignatories { get; set; }


    }
}
