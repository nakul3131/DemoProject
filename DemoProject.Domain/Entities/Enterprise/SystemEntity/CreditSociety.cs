using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.SystemEntity
{
    [Table("CreditSociety")]
    public partial class CreditSociety
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CreditSociety()
        {
            CreditSocietyMakerCheckers = new HashSet<CreditSocietyMakerChecker>();
            CreditSocietyTranslations = new HashSet<CreditSocietyTranslation>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid CreditSocietyId { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfCreditSociety { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        [Required]
        [StringLength(50)]
        public string RegistrationNumber { get; set; }

        [Required]
        [StringLength(2500)]
        public string FullAddressDetail { get; set; }

        [Required]
        [StringLength(500)]
        public string ContactDetails { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? MergeDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CreditSocietyMakerChecker> CreditSocietyMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CreditSocietyTranslation> CreditSocietyTranslations { get; set; }
    }
}
