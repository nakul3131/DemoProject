using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Enterprise.Office
{
    public class BusinessOfficeTransactionParameterViewModel
    {
        // BusinessOfficeTransactionParameter
        public byte PrmKey { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public DateTime EffectiveDate { get; set; }

        public bool EnableAutoGenerateTransactionNumber { get; set; }

        [StringLength(20)]
        public string TransactionNumberMask { get; set; }

        public int StartTransactionNumber { get; set; }

        public int EndTransactionNumber { get; set; }

        public int TransactionNumberIncrementBy { get; set; }

        [StringLength(3)]
        public string TransactionNumberReset { get; set; }

        public bool EnableRegenerateUnusedTransactionNumber { get; set; }

        public bool EnableTransactionDigitalCode { get; set; }

        public byte ChecksumAlgorithmPrmKey { get; set; }

        //    public short FrequencyPrmKey { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // BusinessOfficeTransactionParameterMakerChecker

        public DateTime EntryDateTime { get; set; }

        public byte BusinessOfficeTransactionParameterPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Other
        public string[] MaskTypeCharacterForTransactionNumberMaskId { get; set; }

        public string[] MaskTypeCharacterForTransactionNumberMask { get; set; } 

        public Guid ChecksumAlgorithmId { get; set; }

        public Guid FrequencyId { get; set; }
        
    }
}
