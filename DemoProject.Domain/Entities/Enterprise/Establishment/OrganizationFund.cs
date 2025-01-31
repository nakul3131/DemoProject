using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.Establishment
{
    [Table("OrganizationFund")]
    public partial class OrganizationFund
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrganizationFund()
        {
            OrganizationFundMakerCheckers = new HashSet<OrganizationFundMakerChecker>();
            OrganizationFundTranslations = new HashSet<OrganizationFundTranslation>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid OrganizationFundId { get; set; }

        public short FundPrmKey { get; set; }

        public short SequenceNumber { get; set; }

        [Required]
        [StringLength(20)]
        public string SequenceNumberText { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

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
        public virtual ICollection<OrganizationFundMakerChecker> OrganizationFundMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrganizationFundTranslation> OrganizationFundTranslations { get; set; }
    }
}
