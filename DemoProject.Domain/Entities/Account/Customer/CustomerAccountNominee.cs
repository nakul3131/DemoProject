using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.PersonInformation.SystemEntity;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerAccountNominee")]
    public partial class CustomerAccountNominee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerAccountNominee()
        {
            CustomerAccountNomineeGuardians = new HashSet<CustomerAccountNomineeGuardian>();
            CustomerAccountNomineeMakerCheckers = new HashSet<CustomerAccountNomineeMakerChecker>();
            CustomerAccountNomineeTranslations = new HashSet<CustomerAccountNomineeTranslation>();
        }

        [Key]
        public long PrmKey { get; set; }
        
        public long CustomerAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string NominationNumber { get; set; }

        public DateTime NominationDate { get; set; }

        public int SequenceNumber { get; set; }

        [Required]
        [StringLength(150)]
        public string NameOfNominee { get; set; }
        
        public long NomineePersonInformationNumber { get; set; }

        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(500)]
        public string FullAddressDetails { get; set; }

        [Required]
        [StringLength(150)]
        public string ContactDetails { get; set; }

        public byte RelationPrmKey { get; set; }

        public decimal HoldingPercentage { get; set; }

        public decimal ProportionateAmountForEachNominee { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

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

        public virtual CustomerAccount CustomerAccount { get; set; }

        public virtual Relation Relation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAccountNomineeGuardian> CustomerAccountNomineeGuardians { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAccountNomineeMakerChecker> CustomerAccountNomineeMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAccountNomineeTranslation> CustomerAccountNomineeTranslations { get; set; }
    }
}
