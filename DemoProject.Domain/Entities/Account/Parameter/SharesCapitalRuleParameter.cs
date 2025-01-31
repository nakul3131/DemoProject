using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Parameter
{
    [Table("SharesCapitalRuleParameter")]
    public partial class SharesCapitalRuleParameter
    {
        [Key]
        public byte PrmKey { get; set; }

        public Guid SharesCapitalRuleParameterId { get; set; }

        public byte ModificationNumber { get; set; }

        public DateTime EffectiveDate { get; set; }

        public byte EducationAndTrainingPeriodForBODMembers { get; set; }

        public byte SharesWithdrawalNoticeTimePeriod { get; set; }

        public decimal SharesRefundAmountTimePeriod { get; set; }

        public bool EnableSharesValuation { get; set; }

        public bool IsSharesPaymentExceedFaceValue { get; set; }

        public byte SharesTransferNoticePeriod { get; set; }

        public bool EnableNomination { get; set; }

        public bool EnableMultiplePersonsNomination { get; set; }

        public bool IsRequiredWitnessForNomination { get; set; }

        public bool EnableNominationRevoke { get; set; }

        public bool EnableNominationVary { get; set; }

        public bool EnableNominationRegister { get; set; }

        public decimal DocumentCopyFeesUpto200Words { get; set; }

        public decimal DocumentCopyFeesPerPageForA4Size { get; set; }

        public decimal DocumentCopyFeesPerPageForLargerThanA4Size { get; set; }

        public bool EnableIssuingReceipt { get; set; }

        public bool EnableForfeitureOfSharesOnExpulsionOfMembership { get; set; }

        public byte NoticePeriodOfExpulsionOfMemberBeforeGM { get; set; }

        public byte MinimumEducationAndTrainingPeriodForMembers { get; set; }

        public byte MaximumEducationAndTrainingPeriodForMembers { get; set; }

        public byte MinimumEducationAndTrainingPeriodForBODMembers { get; set; }

        public byte MaximumEducationAndTrainingPeriodForBODMembers { get; set; }

        public byte MinimumEducationAndTrainingPeriodForOfficers { get; set; }

        public byte MaximumEducationAndTrainingPeriodForOfficers { get; set; }

        public byte MinimumEducationAndTrainingPeriodForEmployee { get; set; }

        public byte MaximumEducationAndTrainingPeriodForEmployee { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }
    }
}
