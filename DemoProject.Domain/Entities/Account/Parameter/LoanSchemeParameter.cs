using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Parameter
{
    [Table("LoanSchemeParameter")]
    public partial class LoanSchemeParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoanSchemeParameter()
        {
            LoanSchemeParameterMakerCheckers = new HashSet<LoanSchemeParameterMakerChecker>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte PrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public DateTime EffectiveDate { get; set; }

        public bool EnableLockInPeriodParameter { get; set; }

        public bool EnableTenureListParameter { get; set; }

        public bool EnableApplicationParameter { get; set; }

        public bool EnablePassbookParameter { get; set; }

        public bool EnableDocumentParameter { get; set; }

        public bool EnableLoanPreFullPaymentParameter { get; set; }

        public bool EnableLoanPrePartPaymentParameter { get; set; }

        public bool EnableLoanInterestRebateParameter { get; set; }

        public bool EnableLoanFunderParameter { get; set; }

        public bool EnableLoanDistributorParameter { get; set; }

        public bool EnableRecoveryOfficerIncentiveParameter { get; set; }

        public bool EnableBusinessOfficeParameter { get; set; }

        public bool EnableBankingChannelParameter { get; set; }

        public bool EnableTargetGroupParameter { get; set; }

        public bool EnableSmsServiceParameter { get; set; }

        public bool EnableEmailServiceParameter { get; set; }

        public bool EnableNoticeScheduleParameter { get; set; }

        public bool EnableReportFormatParameter { get; set; }

        public bool EnableTargetEstimationParameter { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LoanSchemeParameterMakerChecker> LoanSchemeParameterMakerCheckers { get; set; }
    }
}
