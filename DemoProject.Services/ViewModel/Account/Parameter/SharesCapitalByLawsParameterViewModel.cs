using System;
using System.ComponentModel.DataAnnotations;
using DemoProject.Services.Abstract.Account.SystemEntity;
using System.Web.Mvc;

namespace DemoProject.Services.ViewModel.Account.Parameter
{
    public class SharesCapitalByLawsParameterViewModel
    {
        private readonly IAccountDetailRepository accountDetailRepository;

        public SharesCapitalByLawsParameterViewModel() 
        {
            accountDetailRepository = DependencyResolver.Current.GetService<IAccountDetailRepository>();
        }

        public byte PrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public DateTime EffectiveDate { get; set; }

        public decimal SharesFaceValue { get; set; }

        public decimal SharesValueOnRedemption { get; set; }

        public decimal AdmissionFeesForMembership { get; set; }

        public decimal AdmissionFeesForNominalMember { get; set; }

        public bool EnableOtherFees1 { get; set; }

        public short OtherFeesGeneralLedgerPrmKey1 { get; set; }

        [StringLength(50)]
        public string TitleForFees1 { get; set; }

        public decimal FeesAmount1 { get; set; }

        public short NominalMembershipValidity { get; set; }

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

        public bool EnableStandardBorrowerForActiveMembership { get; set; }

        public bool EnableVotingRigthsToActiveArrearsMember { get; set; }

        public decimal FeesForSharesTransfer { get; set; }

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

        public short MemberLiabilityPeriodAfterCessation { get; set; }

        public short EstateOfDeceasedMemberLiablePeriod { get; set; }

        public bool EnableIndemnityBondForDuplicateShareCertificate { get; set; }

        public decimal DuplicateShareCertificateIndemnityBondStampAmount { get; set; }

        public decimal DuplicateShareCertificateCharges { get; set; }

        public decimal SharesCapitalToNonMortgageLoanRatio { get; set; }

        public decimal SharesCapitalToMortgageLoanRatio { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // ByLawsSharesCapitalParameterMakerCheker

        public DateTime EntryDateTime { get; set; }

        public byte SharesCapitalByLawsParameterPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Other
        [StringLength(50)]
        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        public short MemberAdmissionFeeGeneralLedgerPrmKey => accountDetailRepository.GetMemberAdmissionFeeAccountClassPrmKey();
    }
}
