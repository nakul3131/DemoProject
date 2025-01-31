using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.Office
{
    [Table("Branch")]
    public partial class Branch
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Branch()
        {
            BranchMakerCheckers = new HashSet<BranchMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid BranchId { get; set; }

        public short BankPrmKey { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfBranch { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        [Required]
        [StringLength(2500)]
        public string FullAddressDetails { get; set; }

        public short CityPrmKey { get; set; }

        [Required]
        [StringLength(1500)]
        public string ContactDetails { get; set; }

        [Required]
        [StringLength(20)]
        public string BranchCode { get; set; }

        [Required]
        [StringLength(11)]
        public string IFSCCode { get; set; }

        public int MICRCode { get; set; }

        [Required]
        [StringLength(11)]
        public string AlphaNumericSWIFTAddress { get; set; }

        [Required]
        [StringLength(20)]
        public string AlphaNumericTelexAddress { get; set; }

        [Required]
        [StringLength(20)]
        public string BranchUniqueIdentityNumberForATM { get; set; }

        [Required]
        [StringLength(20)]
        public string RoutingNumberForClearingTransaction { get; set; }

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
        public virtual ICollection<BranchMakerChecker> BranchMakerCheckers { get; set; }
    }
}
