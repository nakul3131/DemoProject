using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{

    [Table("SchemeSharesCapitalDividendParameter")]
    public partial class SchemeSharesCapitalDividendParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeSharesCapitalDividendParameter()
        {
            SchemeSharesCapitalDividendParameterMakerCheckers = new HashSet<SchemeSharesCapitalDividendParameterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public DateTime EffectiveDate { get; set; }

        public byte FinancialYearForSharesBalance { get; set; }

        public short MembershipAgeForDividend { get; set; }

        public short ExMemberAgeForDividend { get; set; }

        public decimal MinimumDividendPercentage { get; set; }

        public decimal MaximumDividendPercentage { get; set; }

        public byte DividendCalculationMethodPrmKey { get; set; }

        [StringLength(3)]
        public string RoundMethod { get; set; }

        public byte RoundNearest { get; set; }

        public short TimePeriodToCeaseUnclaimedDividend { get; set; }

        //[Required]
        [StringLength(3)]
        public string CeasedDefaulterDividendAction { get; set; }

        //[Required]
        [StringLength(3)]
        public string CeasedDefaulterGuarantorDividendAction { get; set; }

        public bool EnableAccountCustomisation { get; set; }

        public bool EnableDividendAmountCustomisation { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Scheme Scheme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeSharesCapitalDividendParameterMakerChecker> SchemeSharesCapitalDividendParameterMakerCheckers { get; set; }
    }
}
