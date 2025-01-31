using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeAccountParameterViewModel
    {
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

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //SchemeAccountParameterMakerChecker
        public DateTime EntryDateTime { get; set; }

        public short SchemeAccountParameterPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        //Other

        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }
    }
}
