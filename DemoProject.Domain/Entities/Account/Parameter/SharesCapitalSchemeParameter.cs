using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Parameter
{   
    [Table("SharesCapitalSchemeParameter")]
    public partial class SharesCapitalSchemeParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SharesCapitalSchemeParameter()
        {
            SharesCapitalSchemeParameterMakerCheckers = new HashSet<SharesCapitalSchemeParameterMakerChecker>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte PrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public DateTime EffectiveDate { get; set; }

        public bool EnableBankingChannelParameter { get; set; }

        public bool EnableSmsServiceParameter { get; set; }

        public bool EnableEmailServiceParameter { get; set; }

        public bool EnableApplicationParameter { get; set; }

        public bool EnableBusinessOfficeParameter { get; set; }

        public bool EnableClosingChargesParameter { get; set; }

        public bool EnableSharesTransferChargesParameter { get; set; }

        public bool EnableNoticeScheduleParameter { get; set; }

        public bool EnableReportFormatParameter { get; set; }

        public bool EnableTargetEstimationParameter { get; set; }

        public bool EnableLimitParameter { get; set; }

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
        public virtual ICollection<SharesCapitalSchemeParameterMakerChecker> SharesCapitalSchemeParameterMakerCheckers { get; set; }
    }
}
