using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Security.UserRoles
{
    [Table("RoleProfileTransactionLimit")]
    public partial class RoleProfileTransactionLimit
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RoleProfileTransactionLimit()
        {
            RoleProfileTransactionLimitMakerCheckers = new HashSet<RoleProfileTransactionLimitMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short RoleProfilePrmKey { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public int TransactionTypePrmKey { get; set; }

        public short CurrencyPrmKey { get; set; }

        public decimal MinimumAmountLimitForTransaction { get; set; }

        public decimal MaximumAmountLimitForTransaction { get; set; }

        public short MaximumNumberOfBackDaysForTransaction { get; set; }

        public decimal MinimumAmountLimitForVerification { get; set; }

        public decimal MaximumAmountLimitForVerification { get; set; }

        public short MaximumNumberOfBackDaysForVerification { get; set; }

        public decimal MinimumAmountLimitForAutoVerification { get; set; }

        public decimal MaximumAmountLimitForAutoVerification { get; set; }

        public short MaximumNumberOfBackDaysForAutoVerification { get; set; }

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

        public virtual RoleProfile RoleProfile { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoleProfileTransactionLimitMakerChecker> RoleProfileTransactionLimitMakerCheckers { get; set; }
    }
}
