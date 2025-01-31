using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeAccountParameter")]
    public partial class SchemeAccountParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeAccountParameter()
        {
            SchemeAccountParameterMakerCheckers = new HashSet<SchemeAccountParameterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public bool EnableApplication { get; set; }

        public bool EnableAlternateAccountNumber2 { get; set; }

        public bool EnableAlternateAccountNumber3 { get; set; }

        public bool EnableTenure { get; set; }

        public bool EnableTenureList { get; set; }

        public bool EnableMaturityDate { get; set; }

        public bool EnableMaturityOnWorkingDay { get; set; }

        public bool EnableMaturityDateOverride { get; set; }

        public byte MinimumOverridePeriod { get; set; }

        public byte MaximumOverridePeriod { get; set; }

        public bool EnableNumberOfJointAccountHoldingLimit { get; set; }

        public byte MinimumJointAccountHolder { get; set; }

        public byte MaximumJointAccountHolder { get; set; }

        public byte DefaultJointAccountHolder { get; set; }

        public bool EnableNumberOfNomineeLimit { get; set; }

        public byte MinimumNominee { get; set; }

        public byte MaximumNominee { get; set; }

        public byte DefaultNominee { get; set; }

        public bool EnableNomineeSharesHoldingPercentage { get; set; }

        public bool EnableTargetGroup { get; set; }

        public bool EnableInsuranceDetail { get; set; }

        public bool EnablePassbookDetail { get; set; }

        public bool EnableChequeBook { get; set; }

        public bool EnableStandingInstruction { get; set; }

        public bool EnableAdditionalNoteInCustomerAccount { get; set; }

        public bool EnableDigitalCodeForCustomerAccount { get; set; }

        public bool EnableTransferCustomerAccountsInOtherBranch { get; set; }

        public bool EnableOtherFundSubscription { get; set; }

        public bool EnableClosingCharges { get; set; }

        public bool EnableOtherCharges { get; set; }

        public bool EnableSmsService { get; set; }

        public bool EnableEmailService { get; set; }

        public bool EnableDocumentUpload { get; set; }

        public short TimePeriodForNewCustomerFlag { get; set; }

        public bool EnableTDSDeductionOfCashTransaction { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Scheme Scheme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeAccountParameterMakerChecker> SchemeAccountParameterMakerCheckers { get; set; }
    }
}
