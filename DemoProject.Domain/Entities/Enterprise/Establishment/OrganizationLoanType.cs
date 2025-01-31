using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.Establishment
{
    [Table("OrganizationLoanType")]
    public partial class OrganizationLoanType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrganizationLoanType()
        {
            OrganizationLoanTypeMakerCheckers = new HashSet<OrganizationLoanTypeMakerChecker>();
            OrganizationLoanTypeTranslations = new HashSet<OrganizationLoanTypeTranslation>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid OrganizationLoanTypeId { get; set; }

        public byte LoanTypePrmKey { get; set; }

        public byte MaximumLoanTenure { get; set; }

        public decimal MinimumDownPaymentPercentage { get; set; }

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
        public virtual ICollection<OrganizationLoanTypeMakerChecker> OrganizationLoanTypeMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrganizationLoanTypeTranslation> OrganizationLoanTypeTranslations { get; set; }
    }
}
