using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Parameter
{
    [Table("SharesCapitalByLawsParameter")]
    public partial class SharesCapitalByLawsParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SharesCapitalByLawsParameter()
        {
            SharesCapitalByLawsParameterMakerCheckers = new HashSet<SharesCapitalByLawsParameterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public DateTime EffectiveDate { get; set; }

        public decimal SharesFaceValue { get; set; }

        public decimal SharesValueOnRedemption { get; set; }

        public decimal AdmissionFeesForMembership { get; set; }

        public decimal AdmissionFeesForNominalMember { get; set; }

        public short NominalMembershipValidity { get; set; }

        public bool EnableApprovalForEmployeeNomination { get; set; }

        public bool EnableOtherFees1 { get; set; }

        public short OtherFeesGeneralLedgerPrmKey1 { get; set; }

        [StringLength(50)]
        public string TitleForFees1 { get; set; }

        public decimal FeesAmount1 { get; set; }

        public decimal MemberNomineeVariationCharges { get; set; }

        public decimal MemberNomineeCancellationCharges { get; set; }

        public bool EnableIndemnityBondForLegalRepresentativeOfMember { get; set; }

        public decimal LegalRepresentativeOfMemberIndemnityBondStampAmount { get; set; }

        public short MinimumNumberOfSharesForMembershipApplication { get; set; }

        public byte MembershipApplicationDisposalPeriod { get; set; }

        public decimal MaximumSharesHolidingLimitPercentage { get; set; }

        public decimal MaximumSharesHolidingLimitAmount { get; set; }
        
        public bool EnableAGMAttendanceForActiveMembership { get; set; }

        public byte MinimumAttendanceOfAGMForActiveMember { get; set; }

        public bool EnableMinimumSharesHoldingForActiveMembership { get; set; }

        public short MinimumNumberOfSharesHoldingForActiveMember { get; set; }

        public bool EnableDepositHoldingForActiveMembership { get; set; }

        public decimal AggregateDepositsForActiveMember { get; set; }

        public short AggregateDepositsTimePeriodForActiveMember { get; set; }

        public bool EnableBorrowingLoanForActiveMembership { get; set; }

        public decimal MinimumBorrowingAmountForActiveMembership { get; set; }

        public short BorrowingTimePeriodForActiveMembership { get; set; }

        public bool EnableEnjoyingOtherServicesForActiveMembership { get; set; }

        public decimal MinimumUtilizationOfOtherServicesForActiveMember { get; set; }

        public short OtherServicesUtilizationPeriodForActiveMember { get; set; }

        public bool EnablePartialSharesTransfer { get; set; }

        public short MinimumMembershipAgeForPartialSharesTransfer { get; set; }

        public bool EnablePartialSharesWithdrawal { get; set; }

        public short MinimumMembershipAgeForPartialSharesWithdrawal { get; set; }

        public short NoticePeriodForSharesWithdrawal { get; set; }

        public decimal AggregateSharesWithdrawalLimit { get; set; }

        public short MembershipAgeForResignMembership { get; set; }

        public byte MembershipWaitingPeriodBeforeAGM { get; set; }

        public short MembershipWaitingPeriodAfterResignation { get; set; }

        public short MembershipWaitingPeriodAfterExpulsion { get; set; }

        public short MembershipWaitingPeriodAfterDisqualification { get; set; }

        public short EstateOfDeceasedMemberLiablePeriod { get; set; }

        public bool EnableIndemnityBondForDuplicateShareCertificate { get; set; }

        public decimal DuplicateShareCertificateIndemnityBondStampAmount { get; set; }

        public decimal DuplicateShareCertificateCharges { get; set; }

        public decimal SharesCapitalToNonMortgageLoanRatio { get; set; }

        public decimal SharesCapitalToMortgageLoanRatio { get; set; }

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
        public virtual ICollection<SharesCapitalByLawsParameterMakerChecker> SharesCapitalByLawsParameterMakerCheckers { get; set; }
    }
}
