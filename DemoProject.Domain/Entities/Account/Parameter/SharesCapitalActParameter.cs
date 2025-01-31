using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Parameter
{
    [Table("SharesCapitalActParameter")]
    public partial class SharesCapitalActParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SharesCapitalActParameter()
        {
            SharesCapitalActParameterMakerCheckers = new HashSet<SharesCapitalActParameterMakerChecker>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte PrmKey { get; set; }

        public Guid SharesCapitalActParameterId { get; set; }

        public byte ModificationNumber { get; set; }

        public DateTime EffectiveDate { get; set; }

        public byte ValidPersonAgeForMembership { get; set; }

        public byte DisposalValidityOfMembershipApplicationSubmittedByMember { get; set; }

        public byte DisposalValidityOfMembershipApplicationSubmittedByRegistrar { get; set; }

        public byte AppealPeriodForRefusingMembership { get; set; }

        public bool EnableNominalMembership { get; set; }

        public bool EnableAssociateMembership { get; set; }

        public bool EnableEntitlementOfNominalMemberInSharesOrAsset { get; set; }

        public bool EnableEducationAndTrainingToMember { get; set; }

        public bool EnableEducationAndTrainingToOfficers { get; set; }

        public bool EnableEducationAndTrainingToEmployee { get; set; }

        public bool EnableContributionForCoOperativeEducationAndTrainingFund { get; set; }

        public bool EnableCessationOfMembership { get; set; }

        public bool EnableRemovalOfNamesOfMembersFromMembershipRegister { get; set; }

        public byte TotalConsecutivePeriodOfGeneralBodyMeeting { get; set; }

        public byte MinimumAttendanceOfGeneralBodyMeetingForActiveMembership { get; set; }

        public bool EnableMemberAbsenceCondonation { get; set; }

        public bool EnableActiveInactiveMembershipClassiffication { get; set; }

        public byte TimePeriodToCommunicateWithNonActiveMemberAfterFY { get; set; }

        public bool EnableExplusionOfNonActiveMember { get; set; }

        public byte TimePeriodForAppealOnMembershipClassificationAfterCommunication { get; set; }

        public bool IsEligibleForVotingIfNotDeclaredIneligibleToVote { get; set; }

        public byte MembershipLifeOfFederalSocietyForVoting { get; set; }

        public byte MembershipLifeOfIndividualMemberForVoting { get; set; }

        public bool HasVotingRightsToNominalMember { get; set; }

        public bool IsDefaulterEligibleForVoting { get; set; }

        public decimal MaximumSharesHoldingPercentageOtherThanGov { get; set; }

        public decimal MaximumSharesHoldingAmountOtherThanGov { get; set; }

        public byte MinimumSharesHoldingAgeForTransfer { get; set; }

        public bool IsApplicationMandatoryForSharesTransfer { get; set; }

        public decimal MaximumShareReturnPercentageOfExpelledMembersInFinancialYear { get; set; }

        public byte FinancialYearEndDay { get; set; }

        public byte FinancialYearEndMonth { get; set; }

        public byte NoticePeriodForStateGovernmentToWithdrawSharesAmount { get; set; }

        public bool EnableSharesTransferOfDeceasedMember { get; set; }

        public bool EnableFeesForCopyOfDocument { get; set; }

        public byte TimePeriodToProvideRequestedCopyOfDocument { get; set; }

        public bool EnableFurnishPassbookForLoanHolder { get; set; }

        public byte TimePeriodForLiabilityOfDeceasedMember { get; set; }

        public decimal MajorityForExpulsionOfMember { get; set; }

        public byte AdmissionPeriodOfExpelledMember { get; set; }

        public bool IsOverrideAdmissionPeriodOfExpelledMembersByRegistrar { get; set; }

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
        public virtual ICollection<SharesCapitalActParameterMakerChecker> SharesCapitalActParameterMakerCheckers { get; set; }
    }
}
