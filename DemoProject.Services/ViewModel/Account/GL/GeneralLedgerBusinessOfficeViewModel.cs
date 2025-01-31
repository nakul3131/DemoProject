using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.GL
{
    public class GeneralLedgerBusinessOfficeViewModel
    {

        public short PrmKey { get; set; }

        public Guid GeneralLedgerBusinessOfficeId { get; set; }

        public byte ModificationNumber { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public short BusinessOfficePrmKey { get; set; }

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

        //GeneralLedgerBusinessOfficeMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short GeneralLedgerBusinessOfficePrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        //BusinessOffice
        public Guid BusinessOfficeId { get; set; }

        public Guid[] MultiBusinessOfficeId { get; set; }

        [StringLength(100)]
        public string NameOfBusinessOffice { get; set; }
    }
}
