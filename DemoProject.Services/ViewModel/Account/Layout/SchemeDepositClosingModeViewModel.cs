using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeDepositClosingModeViewModel
    {
        // SchemeDepositClosingMode

        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public byte PayInPayOutModePrmKey { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(3)]
        public string ActivationStatus { get; set; }

        // SchemeDepositClosingModeMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short SchemeDepositClosingModePrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Other

        public Guid PayInPayOutModeId { get; set; }
    }
}
