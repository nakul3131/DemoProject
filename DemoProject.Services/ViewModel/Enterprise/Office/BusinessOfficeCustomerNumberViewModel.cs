using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Enterprise.Office
{
    public class BusinessOfficeCustomerNumberViewModel
    {
        // BusinessOfficeCustomerNumber

        public short PrmKey { get; set; }

        public Guid BusinessOfficeCustomerNumberId { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public int StartCustomerNumber { get; set; }

        public int EndCustomerNumber { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // BusinessOfficeCustomerNumberMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short BusinessOfficeCustomerNumberPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }
    }
}
