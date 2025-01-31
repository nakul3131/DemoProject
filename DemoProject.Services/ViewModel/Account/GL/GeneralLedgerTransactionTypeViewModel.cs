using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.GL
{
    public class GeneralLedgerTransactionTypeViewModel
    {
        // GeneralLedgerTransactionType

        public short PrmKey { get; set; }

        public Guid GeneralLedgerTransactionTypeId { get; set; }

        public byte ModificationNumber { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public byte TransactionTypePrmKey { get; set; }

        public decimal MinimumAmountLimit { get; set; }

        public decimal MaximumAmountLimit { get; set; }

        public short AllowedMaximumNumberOfBackDaysEntry { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(3)]
        public string ActivationStatus { get; set; }

        // GeneralLedgerTransactionTypeMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short GeneralLedgerTransactionTypePrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        public string Remark { get; set; }

        // TransactionType
        public Guid TransactionTypeId { get; set; }

        public Guid[] MultiTransactionTypeId { get; set; }

        [StringLength(100)]
        public string NameOfTransactionType { get; set; }

        // Other

        [StringLength(50)]
        public string NameOfUser { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }
    }
}