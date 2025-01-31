using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.PersonInformation.SystemEntity;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerAccountNomineeGuardian")]
    public partial class CustomerAccountNomineeGuardian
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerAccountNomineeGuardian()
        {
            CustomerAccountNomineeGuardianMakerCheckers = new HashSet<CustomerAccountNomineeGuardianMakerChecker>();
            CustomerAccountNomineeGuardianTranslations = new HashSet<CustomerAccountNomineeGuardianTranslation>();
        }

        [Key]
        public int PrmKey { get; set; }
        
        public long CustomerAccountNomineePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string NominationNumber { get; set; }

        [Required]
        [StringLength(150)]
        public string FullName { get; set; }

        [Required]
        [StringLength(50)]
        public string PersonInformationNumber { get; set; }

        public byte GuardianTypePrmKey { get; set; }

        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(150)]
        public string FullAddress { get; set; }

        [Required]
        [StringLength(150)]
        public string ContactDetails { get; set; }

        [Required]
        [StringLength(3)]
        public string AgeProofSubmissionStatusOfTheMinor { get; set; }

        public DateTime AppointedDateOfContact { get; set; }

        public TimeSpan AppointedTimeOfContact { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        public virtual CustomerAccountNominee CustomerAccountNominee { get; set; }

        public virtual GuardianType GuardianType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAccountNomineeGuardianMakerChecker> CustomerAccountNomineeGuardianMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAccountNomineeGuardianTranslation> CustomerAccountNomineeGuardianTranslations { get; set; }
    }
}
