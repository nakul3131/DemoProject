using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("GuardianPerson")]
    public partial class GuardianPerson
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GuardianPerson()
        {
            GuardianPersonMakerCheckers = new HashSet<GuardianPersonMakerChecker>();
            GuardianPersonTranslations  = new HashSet<GuardianPersonTranslation>();
        }

        [Key]
        public long PrmKey { get; set; }

        //public Guid GuardianPersonId { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public long PersonInformationNumber { get; set; }

        [Required]
        [StringLength(150)]
        public string GuardianFullName { get; set; }

        [Required]
        [StringLength(500)]
        public string FullAddress { get; set; }

        public byte RelationPrmKey { get; set; }

        [Required]
        [StringLength(1)]
        public string AgeProofSubmissionStatusOfTheMinor { get; set; }

        public DateTime AppointedDateOfContact { get; set; }

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
        public virtual ICollection<GuardianPersonMakerChecker> GuardianPersonMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GuardianPersonTranslation> GuardianPersonTranslations { get; set; }
    }
}
