using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.Office
{
    [Table("BusinessOfficeModification")]
    public partial class BusinessOfficeModification
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BusinessOfficeModification()
        {
            BusinessOfficeModificationMakerCheckers = new HashSet<BusinessOfficeModificationMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

     //   public Guid BusinessOfficeModificationId { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [Required]
        [StringLength(3)]
        public string BusinessOfficeCode { get; set; }

        [Required]
        [StringLength(10)]
        public string AlternateBusinessOfficeCode { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfBusinessOffice { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfBusinessOfficeForThirdPartyInterface { get; set; }

        public DateTime OpeningDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(500)]
        public string ContactDetails { get; set; }

        [Required]
        [StringLength(1500)]
        public string AddressDetails { get; set; }

        public bool IsFundBranch { get; set; }

        public bool EnableCorporateAccess { get; set; }

        public byte LoanDirectDebitGenerationDays { get; set; }

        public short ParentBusinessOfficePrmKey { get; set; }

        public short ClearingBusinessOfficePrmKey { get; set; }

        [Required]
        [StringLength(15)]
        public string TransactionCodeForClearing { get; set; }

        public short RegionalOfficePrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string BusinessOfficeStatusForCoreOperation { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual BusinessOffice BusinessOffice { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficeModificationMakerChecker> BusinessOfficeModificationMakerCheckers { get; set; }
    }
}
